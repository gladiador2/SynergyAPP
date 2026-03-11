import type { Request, Response } from 'express';
import { consultarLoteSifen, obtenerEstadoProceso, recibirJsonYGenerarXmlInicial } from '../services/sifenService.js';
import { enqueueSifenJob, getSifenQueueSnapshot } from '../services/sifenAsyncQueue.js';
import { HttpError } from '../utils/errors.js';
import { handleError } from '../routes/utils.js';

type RequestWithRawBody = Request & { rawBody?: string };

function extractNumeroLote(req: RequestWithRawBody): string {
    const raw = req.rawBody;
    if (raw) {
        const match = raw.match(/"numeroLote"\s*:\s*"?(\d+)"?/);
        if (match?.[1]) {
            return match[1];
        }
    }

    return String(req.body?.numeroLote ?? '');
}

export async function enviarDEController(req: Request, res: Response): Promise<void> {
    try {
        const inicial = await recibirJsonYGenerarXmlInicial({
            body: req.body,
            usuarioId: 1,
        });

        const queueInfo = enqueueSifenJob({
            jsonId: inicial.jsonId,
            xmlGeneradoId: inicial.xmlGeneradoId,
            xmlConQr: inicial.xmlConQr,
            config: inicial.config,
            mode: 'recibe',
        });

        res.status(202).json({
            success: true,
            message: 'JSON recibido. XML generado y proceso SIFEN encolado.',
            jsonId: inicial.jsonId,
            xmlGenerado: inicial.xmlGenerado,
            estado: 'queued',
            queue: {
                ...queueInfo,
                ...getSifenQueueSnapshot(),
            },
        });
    } catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }

        handleError(res, 500, error);
    }
}

export async function enviarDELoteController(req: Request, res: Response): Promise<void> {
    try {
        const inicial = await recibirJsonYGenerarXmlInicial({
            body: req.body,
            usuarioId: 1,
        });

        const queueInfo = enqueueSifenJob({
            jsonId: inicial.jsonId,
            xmlGeneradoId: inicial.xmlGeneradoId,
            xmlConQr: inicial.xmlConQr,
            config: inicial.config,
            mode: 'recibeLote',
        });

        res.status(202).json({
            success: true,
            message: 'JSON recibido. XML generado y proceso SIFEN por lote encolado.',
            jsonId: inicial.jsonId,
            xmlGenerado: inicial.xmlGenerado,
            estado: 'queued',
            modo: 'recibeLote',
            queue: {
                ...queueInfo,
                ...getSifenQueueSnapshot(),
            },
        });
    } catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }

        handleError(res, 500, error);
    }
}

export async function obtenerEstadoProcesoController(req: Request, res: Response): Promise<void> {
    const jsonId = Number(req.params.jsonId);
    if (!Number.isFinite(jsonId) || jsonId <= 0) {
        handleError(res, 400, 'El parametro jsonId no es valido');
        return;
    }

    try {
        const status = await obtenerEstadoProceso(jsonId);
        res.json({ success: true, data: status });
    } catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }

        handleError(res, 500, error);
    }
}

export async function consultarLoteSifenController(req: Request, res: Response): Promise<void> {
    // Leer desde rawBody evita el redondeo si llega un numero mayor a Number.MAX_SAFE_INTEGER.
    const numeroLote = extractNumeroLote(req as RequestWithRawBody);
   
    const id = req.body?.id !== undefined ? Number(req.body.id) : undefined;

    if (!/^\d+$/.test(numeroLote)) {
        handleError(res, 400, 'El campo numeroLote es obligatorio y debe contener solo digitos');
        return;
    }

    if (id !== undefined && (!Number.isFinite(id) || id <= 0)) {
        handleError(res, 400, 'El campo id debe ser numerico cuando se envia');
        return;
    }

    try {
        const data = await consultarLoteSifen({
            numeroLote,
            config: req.body?.config,
            ...(id !== undefined ? { id } : {}),
        });

        res.json({ success: true, data });
    } catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }

        handleError(res, 500, error);
    }
}
