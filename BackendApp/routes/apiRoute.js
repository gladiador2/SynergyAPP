import { Router } from 'express';
import { consultarLoteSifenController, enviarDEController, enviarDELoteController, obtenerEstadoProcesoController } from '../controllers/apiController.js';
import setapiRoutes from './setapiRoutes.js';
import xmlsignRoutes from './xmlsignRoutes.js';
import kudeRoutes from './kudeRoutes.js';
import qrgenRoutes from './qrgenRoutes.js';
const router = Router();
/**
 * @swagger
 * /API/enviarDE:
 *   post:
 *     tags:
 *       - API
 *     summary: Recibe JSON y responde XML generado en modo asincrono
 *     description: Devuelve inmediatamente el XML y encola el envio a SIFEN con reintentos.
 *     security:
 *       - ApiKeyAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               params:
 *                 type: object
 *               data:
 *                 type: object
 *               config:
 *                 type: object
 *     responses:
 *       202:
 *         description: XML generado y proceso asincrono encolado
 *       500:
 *         description: Error interno
 */
router.post('/enviarDE', enviarDEController);
/**
 * @swagger
 * /API/enviarDELote:
 *   post:
 *     tags:
 *       - API
 *     summary: Recibe JSON y envia a SIFEN usando recibeLote
 *     description: Devuelve inmediatamente el XML y encola el envio por lote a SIFEN.
 *     security:
 *       - ApiKeyAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               params:
 *                 type: object
 *               data:
 *                 type: object
 *               config:
 *                 type: object
 *     responses:
 *       202:
 *         description: XML generado y proceso por lote encolado
 *       500:
 *         description: Error interno
 */
router.post('/enviarDELote', enviarDELoteController);
/**
 * @swagger
 * /API/consultarLote:
 *   post:
 *     tags:
 *       - API
 *     summary: Consulta un numero de lote en SIFEN
 *     description: Ejecuta consultaLote usando configuracion de ambiente y certificado del backend.
 *     security:
 *       - ApiKeyAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - numeroLote
 *             properties:
 *               numeroLote:
 *                 type: number
 *               id:
 *                 type: number
 *               config:
 *                 type: object
 *     responses:
 *       200:
 *         description: Consulta de lote realizada
 *       400:
 *         description: Parametros invalidos
 *       502:
 *         description: Error al consultar SIFEN
 */
router.post('/consultarLote', consultarLoteSifenController);
/**
 * @swagger
 * /API/enviarDE/{jsonId}/estado:
 *   get:
 *     tags:
 *       - API
 *     summary: Consulta el estado del procesamiento asincrono
 *     parameters:
 *       - in: path
 *         name: jsonId
 *         required: true
 *         schema:
 *           type: integer
 *     responses:
 *       200:
 *         description: Estado actual del proceso
 *       404:
 *         description: jsonId no encontrado
 */
router.get('/enviarDE/:jsonId/estado', obtenerEstadoProcesoController);
router.use('/setapi', setapiRoutes);
router.use('/xmlsign', xmlsignRoutes);
router.use('/kude', kudeRoutes);
router.use('/qrgen', qrgenRoutes);
export default router;
//# sourceMappingURL=apiRoute.js.map