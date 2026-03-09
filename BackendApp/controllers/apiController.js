import { obtenerEstadoProceso, recibirJsonYGenerarXmlInicial } from '../services/sifenService.js';
import { enqueueSifenJob, getSifenQueueSnapshot } from '../services/sifenAsyncQueue.js';
import { HttpError } from '../utils/errors.js';
import { handleError } from '../routes/utils.js';
export async function enviarDEController(req, res) {
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
    }
    catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }
        handleError(res, 500, error);
    }
}
export async function obtenerEstadoProcesoController(req, res) {
    const jsonId = Number(req.params.jsonId);
    if (!Number.isFinite(jsonId) || jsonId <= 0) {
        handleError(res, 400, 'El parametro jsonId no es valido');
        return;
    }
    try {
        const status = await obtenerEstadoProceso(jsonId);
        res.json({ success: true, data: status });
    }
    catch (error) {
        if (error instanceof HttpError) {
            handleError(res, error.statusCode, error.message, error.details);
            return;
        }
        handleError(res, 500, error);
    }
}
//# sourceMappingURL=apiController.js.map