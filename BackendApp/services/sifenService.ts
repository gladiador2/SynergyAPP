import xmlgen from 'facturacionelectronicapy-xmlgen';
import apiset from 'facturacionelectronicapy-setapi';
import xmlsign from 'facturacionelectronicapy-xmlsign';
import kude from 'facturacionelectronicapy-kude';
import qr from 'facturacionelectronicapy-qrgen';
import { mkdir, writeFile } from 'node:fs/promises';
import path from 'node:path';
import { parseStringPromise } from 'xml2js';
import { loadKudeConfig, loadSifenConfig, type Ambiente } from '../config/sifenConfig.js';
import {
    getJsonRecibidoStatusById,
    insertJsonRecibido,
    insertJsonRecibidoError,
    insertXmlFirmado,
    insertXmlGenerado,
    updateXmlFirmadoRespuestaByXmlGeneradoId,
    updateJsonRecibidoEstado,
    updateJsonRecibidoError,
} from '../repositories/sifenRepository.js';
import { HttpError, toErrorMessage } from '../utils/errors.js';
import { normalizeXML } from '../utils/xmlUtils.js';

const Kude = kude.default;
const qrgen = qr.default;
const DE = xmlgen.default;
const DESign = xmlsign.default;
const setApiClient = apiset.default;
const XML_PRE_SIFEN_DIR = path.join(process.cwd(), 'logs', 'xml_pre_sifen');

async function saveXmlBeforeSifen(jsonId: number, xml: string): Promise<void> {
    await mkdir(XML_PRE_SIFEN_DIR, { recursive: true });
    const timestamp = new Date().toISOString().replace(/[:.]/g, '-');
    const fileName = `de-${jsonId}.xml`;
    await writeFile(path.join(XML_PRE_SIFEN_DIR, fileName), xml, 'utf8');
}

export interface EnviarDEInput {
    body: {
        params?: Record<string, unknown>;
        data?: Record<string, unknown>;
        config?: Record<string, unknown>;
    };
    usuarioId: number;
}

export interface EnviarDEResult {
    jsonId: number;
    xmlGenerado: string;
    xmlFirmado: string;
    xmlConQr: string;
    respuestaSifen: unknown;
    kude: unknown;
}

export interface EnviarDEInicialResult {
    jsonId: number;
    xmlGeneradoId: number;
    xmlGenerado: string;
    xmlFirmado: string;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}

export interface SifenRetryOptions {
    maxRetries: number;
    baseDelayMs: number;
    maxDelayMs: number;
}

export interface ProcesamientoAsincronoResult {
    respuestaSifen: unknown;
    kude: unknown;
}

function sleep(ms: number): Promise<void> {
    return new Promise((resolve) => setTimeout(resolve, ms));
}

function computeRetryDelay(attempt: number, options: SifenRetryOptions): number {
    const exponential = options.baseDelayMs * Math.pow(2, Math.max(0, attempt - 1));
    const jitter = Math.floor(Math.random() * options.baseDelayMs);
    return Math.min(exponential + jitter, options.maxDelayMs);
}

function isRetryableSifenError(error: unknown): boolean {
    const message = toErrorMessage(error).toLowerCase();
    return [
        'timeout',
        'timed out',
        'econnreset',
        'econnrefused',
        'enotfound',
        'network',
        'socket',
        '503',
        '502',
        '504',
    ].some((signal) => message.includes(signal));
}

function extractSifenStatusAndError(respuestaSifen: unknown): { estado: string; error: string | null } {
    if (!respuestaSifen || typeof respuestaSifen !== 'object') {
        return { estado: 'sifen_unknown', error: null };
    }

    const payload = respuestaSifen as {
        'ns2:rRetEnviDe'?: {
            'ns2:rProtDe'?: {
                'ns2:dEstRes'?: unknown;
                'ns2:gResProc'?: {
                    'ns2:dMsgRes'?: unknown;
                    'ns2:dMsgRe'?: unknown;
                };
            };
        };
    };

    const protDe = payload['ns2:rRetEnviDe']?.['ns2:rProtDe'];
    const estadoRaw = protDe?.['ns2:dEstRes'];
    const msgResRaw = protDe?.['ns2:gResProc']?.['ns2:dMsgRes'];
    const msgReRaw = protDe?.['ns2:gResProc']?.['ns2:dMsgRe'];

    const estado = typeof estadoRaw === 'string' && estadoRaw.trim() !== '' ? estadoRaw.trim() : 'sifen_unknown';
    const error = typeof msgResRaw === 'string' && msgResRaw.trim() !== ''
        ? msgResRaw.trim()
        : typeof msgReRaw === 'string' && msgReRaw.trim() !== ''
            ? msgReRaw.trim()
            : null;

    return { estado, error };
}

function getRetryOptionsFromEnv(): SifenRetryOptions {
    return {
        maxRetries: Number(process.env.SIFEN_MAX_RETRIES ?? 5),
        baseDelayMs: Number(process.env.SIFEN_RETRY_BASE_MS ?? 1500),
        maxDelayMs: Number(process.env.SIFEN_RETRY_MAX_MS ?? 30000),
    };
}

function getLotePollOptionsFromEnv(): { maxPolls: number; pollIntervalMs: number } { // CAMBIO: nueva configuracion de polling para consulta de lote asincrono.
    return { // CAMBIO: devolvemos objeto con limites de espera del lote.
        maxPolls: Number(process.env.SIFEN_LOTE_MAX_POLLS ?? 12), // CAMBIO: maximo de consultas a consultaLote.
        pollIntervalMs: Number(process.env.SIFEN_LOTE_POLL_MS ?? 5000), // CAMBIO: intervalo entre consultas de lote.
    }; // CAMBIO: cierre de configuracion de polling.
} // CAMBIO: cierre de helper para polling asincrono.

function findFirstStringValueByKey(obj: unknown, candidateKeys: string[]): string | null { // CAMBIO: helper generico para extraer campos SIFEN con namespaces variables.
    if (!obj || typeof obj !== 'object') { // CAMBIO: validacion de entrada recursiva.
        return null; // CAMBIO: sin objeto no hay valor a extraer.
    } // CAMBIO: cierre de guard clause.

    const node = obj as Record<string, unknown>; // CAMBIO: casteo para iterar claves dinamicas.
    for (const key of Object.keys(node)) { // CAMBIO: recorremos claves del nodo actual.
        const value = node[key]; // CAMBIO: capturamos valor de la clave actual.
        if (candidateKeys.includes(key) && typeof value === 'string' && value.trim() !== '') { // CAMBIO: si coincide clave objetivo y trae string valido retornamos.
            return value.trim(); // CAMBIO: retorno normalizado del valor encontrado.
        } // CAMBIO: cierre de condicion de match.

        if (value && typeof value === 'object') { // CAMBIO: continuamos busqueda en profundidad.
            const nestedResult = findFirstStringValueByKey(value, candidateKeys); // CAMBIO: recursion para soportar estructuras SOAP variadas.
            if (nestedResult) { // CAMBIO: cortamos cuando ya encontramos el dato.
                return nestedResult; // CAMBIO: retorno temprano del hallazgo recursivo.
            } // CAMBIO: cierre de validacion de resultado recursivo.
        } // CAMBIO: cierre de exploracion recursiva.
    } // CAMBIO: cierre del recorrido de claves.

    return null; // CAMBIO: no se encontro ninguna clave candidata.
} // CAMBIO: cierre de helper de busqueda profunda.

function extractNumeroProtocoloLote(respuesta: unknown): number | null { // CAMBIO: extrae el numero de protocolo devuelto por recibeLote.
    const raw = findFirstStringValueByKey(respuesta, ['ns2:dProtConsLote', 'dProtConsLote']); // CAMBIO: buscamos protocolo con y sin namespace.
    if (!raw) { // CAMBIO: sin protocolo no se puede consultar estado de lote.
        return null; // CAMBIO: devolvemos null para manejarlo arriba.
    } // CAMBIO: cierre de guard clause de protocolo.

    const parsed = Number(raw); // CAMBIO: convertimos protocolo a numero para consultaLote.
    return Number.isFinite(parsed) ? parsed : null; // CAMBIO: validamos conversion numerica segura.
} // CAMBIO: cierre de helper de protocolo.

function isEstadoSifenFinal(estado: string | null): boolean { // CAMBIO: determina si el estado del lote ya es terminal.
    if (!estado) { // CAMBIO: sin estado no puede ser terminal.
        return false; // CAMBIO: retorno explicito para estado inexistente.
    } // CAMBIO: cierre de guard clause.

    const normalized = estado.toLowerCase(); // CAMBIO: normalizamos para comparacion case-insensitive.
    return normalized.includes('aprob') || normalized.includes('rechaz') || normalized.includes('observ'); // CAMBIO: estados terminales mas comunes en SIFEN.
} // CAMBIO: cierre de helper de estado final.

async function enviarASifenConReintentos(
    jsonId: number,
    xmlConQr: string,
    certData: string,
    clave: string,
    ambiente: Ambiente,
    config?: Record<string, unknown>,
): Promise<unknown> {
    const retryOptions = getRetryOptionsFromEnv(); // CAMBIO: mantenemos reintentos para errores de comunicacion.
    const lotePollOptions = getLotePollOptionsFromEnv(); // CAMBIO: opciones de polling para flujo asincrono real.

    for (let attempt = 1; attempt <= retryOptions.maxRetries; attempt++) {
        try {
            const rawEnvioLote = await setApiClient.recibeLote( // CAMBIO: cambiamos envio sincronico por envio asincrono en lote.
                jsonId,
                [xmlConQr], // CAMBIO: recibeLote requiere arreglo de XMLs.
                ambiente,
                certData,
                clave,
                config,
            );

            const numeroProtocoloLote = extractNumeroProtocoloLote(rawEnvioLote); // CAMBIO: obtenemos protocolo para consultas posteriores.
            if (!numeroProtocoloLote) { // CAMBIO: si no hay protocolo devolvemos la respuesta cruda del envio de lote.
                return typeof rawEnvioLote === 'string' // CAMBIO: soportamos respuesta string o objeto.
                    ? await parseStringPromise(rawEnvioLote, { explicitArray: false }) // CAMBIO: parse en caso de XML string.
                    : rawEnvioLote; // CAMBIO: si ya es objeto lo devolvemos.
            } // CAMBIO: cierre de fallback sin protocolo.

            let ultimaConsulta: unknown = rawEnvioLote; // CAMBIO: iniciamos con respuesta de envio de lote.
            for (let pollAttempt = 1; pollAttempt <= lotePollOptions.maxPolls; pollAttempt++) { // CAMBIO: ciclo de consultas hasta estado terminal o timeout.
                const rawConsultaLote = await setApiClient.consultaLote( // CAMBIO: consulta asincrona por numero de protocolo de lote.
                    jsonId,
                    numeroProtocoloLote,
                    ambiente,
                    certData,
                    clave,
                    config,
                );

                const parsedConsulta = typeof rawConsultaLote === 'string' // CAMBIO: normalizamos respuesta de consulta lote.
                    ? await parseStringPromise(rawConsultaLote, { explicitArray: false })
                    : rawConsultaLote;

                ultimaConsulta = parsedConsulta; // CAMBIO: persistimos ultima respuesta para retorno por timeout.
                const estadoConsulta = findFirstStringValueByKey(parsedConsulta, ['ns2:dEstRes', 'dEstRes']); // CAMBIO: buscamos estado de resultado en respuesta de lote.
                if (isEstadoSifenFinal(estadoConsulta)) { // CAMBIO: terminamos cuando SIFEN devuelve estado final.
                    return parsedConsulta; // CAMBIO: retorno inmediato con estado final del lote.
                }

                await updateJsonRecibidoEstado(jsonId, `lote_poll_${pollAttempt}`, null); // CAMBIO: trazabilidad de avance de polling en DB.
                await sleep(lotePollOptions.pollIntervalMs); // CAMBIO: pausa entre consultas para no saturar SIFEN.
            }

            return ultimaConsulta; // CAMBIO: si no hubo estado final en tiempo, retornamos ultima consulta disponible.

        } catch (error) {
            const retryable = isRetryableSifenError(error);
            const isLastAttempt = attempt === retryOptions.maxRetries;

            if (!retryable || isLastAttempt) {
                throw error;
            }

            await updateJsonRecibidoEstado(jsonId, `sifen_retry_${attempt}`, toErrorMessage(error));
            await sleep(computeRetryDelay(attempt, retryOptions));
        }
    }

    throw new Error('No se pudo completar el envio a SIFEN luego de varios reintentos');
}

export async function recibirJsonYGenerarXmlInicial(input: EnviarDEInput): Promise<EnviarDEInicialResult> {
    const { body, usuarioId } = input;
    const { params, data, config } = body;

    const sifenConfig = loadSifenConfig();

    let jsonId: number;

    try {
        jsonId = await insertJsonRecibido(body, 'received', usuarioId);
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await insertJsonRecibidoError(body, 'error', errorMsg, usuarioId);
        throw new HttpError(500, errorMsg);
    }

    let xmlGenerado: string;
    let xmlGeneradoId: number;

    try {
        const rawXml = await DE.generateXMLDE(params, data, config as any);
        xmlGenerado = normalizeXML(rawXml);
        xmlGeneradoId = await insertXmlGenerado(xmlGenerado, jsonId);
        if (!xmlGeneradoId) {
            throw new Error('No se pudo guardar el XML generado');
        }
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await updateJsonRecibidoError(jsonId, 'error', errorMsg);
        throw new HttpError(500, errorMsg, { jsonId });
    }

    let xmlFirmado: string;
    try {
        xmlFirmado = await DESign.signXML(normalizeXML(xmlGenerado), sifenConfig.certData, sifenConfig.clave, true);
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await updateJsonRecibidoError(jsonId, 'error', errorMsg);
        await insertXmlFirmado({
            xmlGeneradoId,
            datosXmlFirmado: xmlGenerado,
            estado: 'error',
            error: errorMsg,
            respuesta: null,
        }).catch(() => undefined);
        throw new HttpError(500, errorMsg, { jsonId });
    }

    let xmlConQr: string;
    try {
        xmlConQr = normalizeXML(
            await qrgen.generateQR(xmlFirmado, sifenConfig.idCSC, sifenConfig.csc, sifenConfig.ambiente),
        );
        await saveXmlBeforeSifen(jsonId, xmlConQr);
        await insertXmlFirmado({
            xmlGeneradoId,
            datosXmlFirmado: normalizeXML(xmlConQr),
            estado: 'success',
            error: null,
            respuesta: null,
        });
        await updateJsonRecibidoEstado(jsonId, 'queued', null);
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await updateJsonRecibidoError(jsonId, 'error', errorMsg);
        throw new HttpError(500, errorMsg, { jsonId });
    }

    return {
        jsonId,
        xmlGeneradoId,
        xmlGenerado,
        xmlFirmado,
        xmlConQr,
        config,
    };
}

export async function procesarFlujoAsincronoSifen(input: {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}): Promise<ProcesamientoAsincronoResult> {
    const sifenConfig = loadSifenConfig();
    const kudeConfig = loadKudeConfig();

    await updateJsonRecibidoEstado(input.jsonId, 'sifen_processing', null);

    let respuestaSifen: unknown;
    try {
        respuestaSifen = await enviarASifenConReintentos(
            input.jsonId,
            input.xmlConQr,
            sifenConfig.certData,
            sifenConfig.clave,
            sifenConfig.ambiente,
            input.config,
        );

        const sifenResult = extractSifenStatusAndError(respuestaSifen);

        await updateXmlFirmadoRespuestaByXmlGeneradoId(
            input.xmlGeneradoId,
            sifenResult.estado,
            JSON.stringify(respuestaSifen),
            sifenResult.error,
        );
        await updateJsonRecibidoEstado(input.jsonId, sifenResult.estado, sifenResult.error);
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await updateJsonRecibidoError(input.jsonId, 'error', errorMsg);
        throw new HttpError(502, `Fallo de comunicacion con SIFEN: ${errorMsg}`, { jsonId: input.jsonId });
    }

    try {
        const jsonParm = `{"logo":"${kudeConfig.logo}"}`;
        const kudeResult = await Kude.generateKUDE(
            kudeConfig.java8Path,
            path.join(XML_PRE_SIFEN_DIR, `de-${input.jsonId}.xml`),
            kudeConfig.srcJasper,
            kudeConfig.destFolder,
            jsonParm,
        );

        return {
            respuestaSifen,
            kude: kudeResult,
        };
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        await updateJsonRecibidoError(input.jsonId, 'error', errorMsg);
        throw new HttpError(500, errorMsg, { jsonId: input.jsonId });
    }
}

export async function enviarDE(input: EnviarDEInput): Promise<EnviarDEResult> {
    const inicial = await recibirJsonYGenerarXmlInicial(input);
    const finalResult = await procesarFlujoAsincronoSifen({
        jsonId: inicial.jsonId,
        xmlGeneradoId: inicial.xmlGeneradoId,
        xmlConQr: inicial.xmlConQr,
        config: inicial.config,
    });

    return {
        jsonId: inicial.jsonId,
        xmlGenerado: inicial.xmlGenerado,
        xmlFirmado: inicial.xmlFirmado,
        xmlConQr: inicial.xmlConQr,
        respuestaSifen: finalResult.respuestaSifen,
        kude: finalResult.kude,
    };
}

export async function obtenerEstadoProceso(jsonId: number): Promise<{ id: number; estado: string; error: string | null; fechaCreacion: Date }> {
    const status = await getJsonRecibidoStatusById(jsonId);
    if (!status) {
        throw new HttpError(404, 'No se encontro el proceso para el jsonId indicado', { jsonId });
    }

    return status;
}
