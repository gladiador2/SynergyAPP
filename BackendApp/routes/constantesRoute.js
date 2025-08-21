import { Router } from 'express';
import xmlgen from 'facturacionelectronicapy-xmlgen';
const constantes = xmlgen.constantes;
const DE = xmlgen.default;
const router = Router();
/**
 * @swagger
 * /Constantes/consultarTiposConstancias:
 *   get:
 *     summary: consultar Tipos Constancias
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de Tipos Constancias disponibles para la facturacion electronica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de Tipos Constancias
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposConstancias', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposConstancias });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCaracteristicasCargas:
 *   get:
 *     summary: Consultar Caracter�sticas de Cargas
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de caracter�sticas de cargas disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de caracter�sticas de cargas
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCaracteristicasCargas', (req, res) => {
    try {
        res.json({ success: true, data: constantes.caracteristicasCargas });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarEventoConformidadTipo:
 *   get:
 *     summary: Consultar Tipos de Evento de Conformidad
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de evento de conformidad disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de evento de conformidad
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarEventoConformidadTipo', (req, res) => {
    try {
        res.json({ success: true, data: constantes.eventoConformidadTipo });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposDocumentosImpresos:
 *   get:
 *     summary: Consultar Tipos de Documentos Impresos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de documentos impresos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de documentos impresos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposDocumentosImpresos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposDocumentosImpresos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposDocumentosAsociados:
 *   get:
 *     summary: Consultar Tipos de Documentos Asociados
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de documentos asociados disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de documentos asociados
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposDocumentosAsociados', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposDocumentosAsociados });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarResponsablesFletes:
 *   get:
 *     summary: Consultar Responsables de Fletes
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de responsables de fletes disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de responsables de fletes
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarResponsablesFletes', (req, res) => {
    try {
        res.json({ success: true, data: constantes.responsablesFletes });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarModalidadesTransportes:
 *   get:
 *     summary: Consultar Modalidades de Transportes
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de modalidades de transportes disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de modalidades de transportes
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarModalidadesTransportes', (req, res) => {
    try {
        res.json({ success: true, data: constantes.modalidadesTransportes });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposTransportes:
 *   get:
 *     summary: Consultar Tipos de Transportes
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de transportes disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de transportes
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposTransportes', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposTransportes });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposCombustibles:
 *   get:
 *     summary: Consultar Tipos de Combustibles
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de combustibles disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de combustibles
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposCombustibles', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposCombustibles });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposOperacionesVehiculos:
 *   get:
 *     summary: Consultar Tipos de Operaciones de Veh�culos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de operaciones de veh�culos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de operaciones de veh�culos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposOperacionesVehiculos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposOperacionesVehiculos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarRelevanciasMercaderias:
 *   get:
 *     summary: Consultar Relevancias de Mercader�as
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de relevancias de mercader�as disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de relevancias de mercader�as
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarRelevanciasMercaderias', (req, res) => {
    try {
        res.json({ success: true, data: constantes.relevanciasMercaderias });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCondicionesNegociaciones:
 *   get:
 *     summary: Consultar Condiciones de Negociaciones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de condiciones de negociaciones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de condiciones de negociaciones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCondicionesNegociaciones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.condicionesNegociaciones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTasasIsc:
 *   get:
 *     summary: Consultar Tasas ISC
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tasas ISC disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tasas ISC
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTasasIsc', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tasasIsc });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCategoriasIsc:
 *   get:
 *     summary: Consultar Categor�as ISC
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de categor�as ISC disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de categor�as ISC
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCategoriasIsc', (req, res) => {
    try {
        res.json({ success: true, data: constantes.categoriasIsc });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCodigosAfectaciones:
 *   get:
 *     summary: Consultar C�digos de Afectaciones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de c�digos de afectaciones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de c�digos de afectaciones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCodigosAfectaciones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.codigosAfectaciones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarUnidadesMedidas:
 *   get:
 *     summary: Consultar Unidades de Medidas
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de unidades de medidas disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de unidades de medidas
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarUnidadesMedidas', (req, res) => {
    try {
        res.json({ success: true, data: constantes.unidadesMedidas });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTarjetasCreditosFormasProcesamiento:
 *   get:
 *     summary: Consultar Formas de Procesamiento de Tarjetas de Cr�dito
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de formas de procesamiento de tarjetas de cr�dito disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de formas de procesamiento de tarjetas de cr�dito
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTarjetasCreditosFormasProcesamiento', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tarjetasCreditosFormasProcesamiento });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTarjetasCreditosTipos:
 *   get:
 *     summary: Consultar Tipos de Tarjetas de Cr�dito
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de tarjetas de cr�dito disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de tarjetas de cr�dito
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTarjetasCreditosTipos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tarjetasCreditosTipos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCondicionesCreditosTipos:
 *   get:
 *     summary: Consultar Tipos de Condiciones de Cr�ditos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de condiciones de cr�ditos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de condiciones de cr�ditos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCondicionesCreditosTipos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.condicionesCreditosTipos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCondicionesTiposPagos:
 *   get:
 *     summary: Consultar Tipos de Condiciones de Pagos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de condiciones de pagos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de condiciones de pagos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCondicionesTiposPagos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.condicionesTiposPagos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarCondicionesOperaciones:
 *   get:
 *     summary: Consultar Condiciones de Operaciones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de condiciones de operaciones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de condiciones de operaciones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarCondicionesOperaciones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.condicionesOperaciones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarRemisionesResponsables:
 *   get:
 *     summary: Consultar Responsables de Remisiones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de responsables de remisiones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de responsables de remisiones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarRemisionesResponsables', (req, res) => {
    try {
        res.json({ success: true, data: constantes.remisionesResponsables });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarRemisionesMotivos:
 *   get:
 *     summary: Consultar Motivos de Remisiones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de motivos de remisiones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de motivos de remisiones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarRemisionesMotivos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.remisionesMotivos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarNotasCreditosMotivos:
 *   get:
 *     summary: Consultar Motivos de Notas de Cr�dito
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de motivos de notas de cr�dito disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de motivos de notas de cr�dito
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarNotasCreditosMotivos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.notasCreditosMotivos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarNaturalezaVendedorAutofactura:
 *   get:
 *     summary: Consultar Naturaleza de Vendedor para Autofactura
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de naturaleza de vendedor para autofactura disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de naturaleza de vendedor para autofactura
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarNaturalezaVendedorAutofactura', (req, res) => {
    try {
        res.json({ success: true, data: constantes.naturalezaVendedorAutofactura });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTipoReceptor:
 *   get:
 *     summary: Consultar Tipos de Receptor
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de receptor disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de receptor
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTipoReceptor', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tipoReceptor });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarIndicadoresPresencias:
 *   get:
 *     summary: Consultar Indicadores de Presencias
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de indicadores de presencias disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de indicadores de presencias
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarIndicadoresPresencias', (req, res) => {
    try {
        res.json({ success: true, data: constantes.indicadoresPresencias });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposOperaciones:
 *   get:
 *     summary: Consultar Tipos de Operaciones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de operaciones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de operaciones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposOperaciones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposOperaciones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposDocumentosReceptor:
 *   get:
 *     summary: Consultar Tipos de Documentos de Receptor
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de documentos de receptor disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de documentos de receptor
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposDocumentosReceptor', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposDocumentosReceptor });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposDocumentosIdentidades:
 *   get:
 *     summary: Consultar Tipos de Documentos de Identidad
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de documentos de identidad disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de documentos de identidad
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposDocumentosIdentidades', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposDocumentosIdentidades });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposRegimenes:
 *   get:
 *     summary: Consultar Tipos de Reg�menes
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de reg�menes disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de reg�menes
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposRegimenes', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposRegimenes });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarGlobalPorItem:
 *   get:
 *     summary: Consultar Global por �tem
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de global por �tem disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de global por �tem
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarGlobalPorItem', (req, res) => {
    try {
        res.json({ success: true, data: constantes.globalPorItem });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposEmisiones:
 *   get:
 *     summary: Consultar Tipos de Emisiones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de emisiones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de emisiones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposEmisiones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposEmisiones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposDocumentos:
 *   get:
 *     summary: Consultar Tipos de Documentos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de documentos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de documentos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposDocumentos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposDocumentos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposTransacciones:
 *   get:
 *     summary: Consultar Tipos de Transacciones
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de transacciones disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de transacciones
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposTransacciones', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposTransacciones });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarMonedas:
 *   get:
 *     summary: Consultar Monedas
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de monedas disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de monedas
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarMonedas', (req, res) => {
    try {
        res.json({ success: true, data: constantes.monedas });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /Constantes/consultarTiposImpuestos:
 *   get:
 *     summary: Consultar Tipos de Impuestos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de impuestos disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de impuestos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposImpuestos', (req, res) => {
    try {
        res.json({ success: true, data: constantes.tiposImpuestos });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
// --- FIN AUTO-GENERATED ENDPOINTS ---
/**
 * @swagger
 * /facturacion/consultarPaises:
 *   get:
 *     summary: Consulta pa�ses
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de pa�ses disponibles para la facturaci�n electr�nica.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de pa�ses
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarPaises', async (req, res) => {
    try {
        const result = await DE.consultarPaises();
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/consultarDepartamentos:
 *   get:
 *     summary: Consulta departamentos
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de departamentos disponibles.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de departamentos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarDepartamentos', async (req, res) => {
    try {
        const result = await DE.consultarDepartamentos();
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/consultarDistritos:
 *   get:
 *     summary: Consulta distritos por departamentoId
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de distritos para el departamento especificado.
 *     security:
 *       - ApiKeyAuth: []
 *     parameters:
 *       - in: query
 *         name: departamentoId
 *         required: true
 *         schema:
 *           type: number
 *           example: 11
 *     responses:
 *       200:
 *         description: Lista de distritos
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       400:
 *         description: Par�metro inv�lido
 *       500:
 *         description: Error interno
 */
router.get('/consultarDistritos', async (req, res) => {
    const departamentoId = Number(req.query.departamentoId);
    if (isNaN(departamentoId)) {
        return res.status(400).json({ error: 'departamentoId es requerido y debe ser un n�mero' });
    }
    try {
        const result = await DE.consultarDistritos(departamentoId);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/consultarCiudades:
 *   get:
 *     summary: Consulta ciudades por distritoId
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de ciudades para el distrito especificado.
 *     security:
 *       - ApiKeyAuth: []
 *     parameters:
 *       - in: query
 *         name: distritoId
 *         required: true
 *         schema:
 *           type: number
 *           example: 3344
 *     responses:
 *       200:
 *         description: Lista de ciudades
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       400:
 *         description: Par�metro inv�lido
 *       500:
 *         description: Error interno
 */
router.get('/consultarCiudades', async (req, res) => {
    // Se espera un par�metro distritoId en la query
    const distritoId = Number(req.query.distritoId);
    if (isNaN(distritoId)) {
        return res.status(400).json({ error: 'distritoId es requerido y debe ser un n�mero' });
    }
    try {
        const result = await DE.consultarCiudades(distritoId);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/consultarTiposRegimenes:
 *   get:
 *     summary: Consulta tipos de regimenes
 *     tags:
 *       - Constantes
 *     description: Retorna la lista de tipos de regimenes disponibles.
 *     security:
 *       - ApiKeyAuth: []
 *     responses:
 *       200:
 *         description: Lista de tipos de regimenes
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *       500:
 *         description: Error interno
 */
router.get('/consultarTiposRegimenes', async (req, res) => {
    try {
        const result = await DE.consultarTiposRegimenes();
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
export default (app) => {
    app.use('/Constantes', router);
};
//# sourceMappingURL=constantesRoute.js.map