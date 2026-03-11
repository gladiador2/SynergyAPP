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
        'ns2:rResEnviLoteDe'?: {
            'ns2:dEstRes'?: unknown;
            'ns2:dMsgRes'?: unknown;
            'ns2:gResProc'?: {
                'ns2:dMsgRes'?: unknown;
                'ns2:dMsgRe'?: unknown;
            };
        };
    };

    const protDe = payload['ns2:rRetEnviDe']?.['ns2:rProtDe'];
    const lote = payload['ns2:rResEnviLoteDe'];
    const estadoRaw = protDe?.['ns2:dEstRes'] ?? lote?.['ns2:dEstRes'];
    const msgResRaw = protDe?.['ns2:gResProc']?.['ns2:dMsgRes'] ?? lote?.['ns2:gResProc']?.['ns2:dMsgRes'] ?? lote?.['ns2:dMsgRes'];
    const msgReRaw = protDe?.['ns2:gResProc']?.['ns2:dMsgRe'] ?? lote?.['ns2:gResProc']?.['ns2:dMsgRe'];

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

async function enviarASifenConReintentos(
    jsonId: number,
    xmlConQr: string,
    certData: string,
    clave: string,
    ambiente: Ambiente,
    config?: Record<string, unknown>,
): Promise<unknown> {
    const retryOptions = getRetryOptionsFromEnv();

    for (let attempt = 1; attempt <= retryOptions.maxRetries; attempt++) {
        try {
            const rawRespuestaSifen = await setApiClient.recibe(
                jsonId,
                xmlConQr,
                ambiente,
                certData,
                clave,
                config,
            );

            const parsed =
                typeof rawRespuestaSifen === 'string'
                    ? await parseStringPromise(rawRespuestaSifen, { explicitArray: false })
                    : rawRespuestaSifen;

            return parsed;
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

async function enviarASifenLoteConReintentos(
    jsonId: number,
    xmlConQr: string,
    certData: string,
    clave: string,
    ambiente: Ambiente,
    config?: Record<string, unknown>,
): Promise<unknown> {
    const retryOptions = getRetryOptionsFromEnv();

    for (let attempt = 1; attempt <= retryOptions.maxRetries; attempt++) {
        try {
            const rawRespuestaSifen = await setApiClient.recibeLote(
                jsonId,
                [xmlConQr],
                ambiente,
                certData,
                clave,
                config,
            );

            const parsed =
                typeof rawRespuestaSifen === 'string'
                    ? await parseStringPromise(rawRespuestaSifen, { explicitArray: false })
                    : rawRespuestaSifen;

            return parsed;
        } catch (error) {
            const retryable = isRetryableSifenError(error);
            const isLastAttempt = attempt === retryOptions.maxRetries;

            if (!retryable || isLastAttempt) {
                throw error;
            }

            await updateJsonRecibidoEstado(jsonId, `sifen_lote_retry_${attempt}`, toErrorMessage(error));
            await sleep(computeRetryDelay(attempt, retryOptions));
        }
    }

    throw new Error('No se pudo completar el envio por lote a SIFEN luego de varios reintentos');
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

export async function procesarFlujoAsincronoSifenLote(input: {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}): Promise<ProcesamientoAsincronoResult> {
    const sifenConfig = loadSifenConfig();
    const kudeConfig = loadKudeConfig();

    await updateJsonRecibidoEstado(input.jsonId, 'sifen_lote_processing', null);

    let respuestaSifen: unknown;
    try {
        respuestaSifen = await enviarASifenLoteConReintentos(
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
        throw new HttpError(502, `Fallo de comunicacion con SIFEN (lote): ${errorMsg}`, { jsonId: input.jsonId });
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

export async function consultarLoteSifen(input: {
    numeroLote: string;
    id?: number;
    config?: Record<string, unknown>;
}): Promise<unknown> {
    const sifenConfig = loadSifenConfig();
    const requestId = input.id ?? Date.now();

    try {
        const raw = await setApiClient.consultaLote(
            requestId,
            input.numeroLote as unknown as number,
            sifenConfig.ambiente,
            sifenConfig.certData,
            sifenConfig.clave,
            input.config,
        );

        return typeof raw === 'string'
            ? await parseStringPromise(raw, { explicitArray: false })
            : raw;
    } catch (error) {
        const errorMsg = toErrorMessage(error);
        throw new HttpError(502, `Fallo al consultar lote en SIFEN: ${errorMsg}`);
    }
}
