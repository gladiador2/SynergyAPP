import { Router } from 'express';
import xmlgen from 'facturacionelectronicapy-xmlgen';
import constantesService from 'facturacionelectronicapy-xmlgen/dist/services/constants.service.js';
import { handleError, sendConstantes } from './utils.js';
const constantes = constantesService;
const DE = xmlgen.default;
const router = Router();
/**
 * @swagger
 * tags:
 *   name: Constantes
 *   description: Endpoints para consultar constantes del sistema
 */
// Funci�n auxiliar para endpoints de constantes
function constantesHandler(prop) {
    return (req, res) => {
        try {
            sendConstantes(res, constantes[prop]);
        }
        catch (error) {
            handleError(res, 500, error);
        }
    };
}
// Lista de endpoints simples
const endpoints = [
    ['/consultarTiposConstancias', 'tiposConstancias'],
    ['/consultarCaracteristicasCargas', 'caracteristicasCargas'],
    ['/consultarEventoConformidadTipo', 'eventoConformidadTipo'],
    ['/consultarTiposDocumentosImpresos', 'tiposDocumentosImpresos'],
    ['/consultarTiposDocumentosAsociados', 'tiposDocumentosAsociados'],
    ['/consultarResponsablesFletes', 'responsablesFletes'],
    ['/consultarModalidadesTransportes', 'modalidadesTransportes'],
    ['/consultarTiposTransportes', 'tiposTransportes'],
    ['/consultarTiposCombustibles', 'tiposCombustibles'],
    ['/consultarTiposOperacionesVehiculos', 'tiposOperacionesVehiculos'],
    ['/consultarRelevanciasMercaderias', 'relevanciasMercaderias'],
    ['/consultarCondicionesNegociaciones', 'condicionesNegociaciones'],
    ['/consultarTasasIsc', 'tasasIsc'],
    ['/consultarCategoriasIsc', 'categoriasIsc'],
    ['/consultarCodigosAfectaciones', 'codigosAfectaciones'],
    ['/consultarUnidadesMedidas', 'unidadesMedidas'],
    ['/consultarTarjetasCreditosFormasProcesamiento', 'tarjetasCreditosFormasProcesamiento'],
    ['/consultarTarjetasCreditosTipos', 'tarjetasCreditosTipos'],
    ['/consultarCondicionesCreditosTipos', 'condicionesCreditosTipos'],
    ['/consultarCondicionesTiposPagos', 'condicionesTiposPagos'],
    ['/consultarCondicionesOperaciones', 'condicionesOperaciones'],
    ['/consultarRemisionesResponsables', 'remisionesResponsables'],
    ['/consultarRemisionesMotivos', 'remisionesMotivos'],
    ['/consultarNotasCreditosMotivos', 'notasCreditosMotivos'],
    ['/consultarNaturalezaVendedorAutofactura', 'naturalezaVendedorAutofactura'],
    ['/consultarTipoReceptor', 'tipoReceptor'],
    ['/consultarIndicadoresPresencias', 'indicadoresPresencias'],
    ['/consultarTiposOperaciones', 'tiposOperaciones'],
    ['/consultarTiposDocumentosReceptor', 'tiposDocumentosReceptor'],
    ['/consultarTiposDocumentosIdentidades', 'tiposDocumentosIdentidades'],
    ['/consultarTiposRegimenes', 'tiposRegimenes'],
    ['/consultarGlobalPorItem', 'globalPorItem'],
    ['/consultarTiposEmisiones', 'tiposEmisiones'],
    ['/consultarTiposDocumentos', 'tiposDocumentos'],
    ['/consultarTiposTransacciones', 'tiposTransacciones'],
    ['/consultarMonedas', 'monedas'],
    ['/consultarTiposImpuestos', 'tiposImpuestos'],
];
// Registrar endpoints simples con anotaciones Swagger din�micas
for (const [path, prop] of endpoints) {
    /**
     * @swagger
     * /Constantes/consultarTiposConstancias:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta tiposConstancias
     *     responses:
     *       200:
     *         description: Lista de tiposConstancias
     *       500:
     *         description: Error interno
    */
    /**
      * @swagger
      * /Constantes/consultarEventoConformidadTipo:
      *   get:
      *     tags: [ConstantesGetSimples]
      *     summary: consultar Evento ConformidadTipo
      *     responses:
      *       200:
      *         description: consultar Evento ConformidadTipo
      *       500:
      *         description: Error interno
    */
    /**
       * @swagger
       * /Constantes/consultarTiposDocumentosAsociados:
       *   get:
       *     tags: [ConstantesGetSimples]
       *     summary: get constantes
       *     responses:
       *       200:
       *         description: get constantes
       *       500:
       *         description: Error interno
    */
    /**
       * @swagger
       * /Constantes/consultarTiposDocumentosImpresos:
       *   get:
       *     tags: [ConstantesGetSimples]
       *     summary: get constantes
       *     responses:
       *       200:
       *         description: get constantes
       *       500:
       *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarModalidadesTransportes:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarTiposTransportes:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarCategoriasIsc:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarCodigosAfectaciones:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
     */
    /**
        * @swagger
        * /Constantes/consultarUnidadesMedidas:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarTarjetasCreditosFormasProcesamiento:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
     */
    /**
        * @swagger
        * /Constantes/consultarTarjetasCreditosTipos:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarCondicionesTiposPagos:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarCondicionesOperaciones:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarCondicionesCreditosTipos:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarRemisionesResponsables:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
        * /Constantes/consultarRemisionesMotivos:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarNotasCreditosMotivos:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarNaturalezaVendedorAutofactura:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarTipoReceptor:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarIndicadoresPresencias:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarTiposOperaciones:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
     */
    /**
        * @swagger
        * /Constantes/consultarTiposDocumentosReceptor:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
          * @swagger
          * /Constantes/consultarTiposDocumentosIdentidades:
          *   get:
          *     tags: [ConstantesGetSimples]
          *     summary: get constantes
          *     responses:
          *       200:
          *         description: get constantes
          *       500:
          *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarTiposRegimenes:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
          * @swagger
          * /Constantes/consultarGlobalPorItem:
          *   get:
          *     tags: [ConstantesGetSimples]
          *     summary: get constantes
          *     responses:
          *       200:
          *         description: get constantes
          *       500:
          *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarTiposEmisiones:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
          * @swagger
          * /Constantes/consultarTiposDocumentos:
          *   get:
          *     tags: [ConstantesGetSimples]
          *     summary: get constantes
          *     responses:
          *       200:
          *         description: get constantes
          *       500:
          *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarTiposTransacciones:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
          * @swagger
          * /Constantes/consultarMonedas:
          *   get:
          *     tags: [ConstantesGetSimples]
          *     summary: get constantes
          *     responses:
          *       200:
          *         description: get constantes
          *       500:
          *         description: Error interno
    */
    /**
        * @swagger
        * /Constantes/consultarMonedas:
        *   get:
        *     tags: [ConstantesGetSimples]
        *     summary: get constantes
        *     responses:
        *       200:
        *         description: get constantes
        *       500:
        *         description: Error interno
    */
    /**
         * @swagger
         * /Constantes/consultarTiposImpuestos:
         *   get:
         *     tags: [ConstantesGetSimples]
         *     summary: get constantes
         *     responses:
         *       200:
         *         description: get constantes
         *       500:
         *         description: Error interno
    */
    /**
     * @swagger
     * /Constantes/consultarTiposCombustibles:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta tiposCombustibles
     *     responses:
     *       200:
     *         description: Lista de tiposCombustibles
     *       500:
     *         description: Error interno
     */
    /**
     * @swagger
     * /Constantes/consultarTiposOperacionesVehiculos:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta tiposOperacionesVehiculos
     *     responses:
     *       200:
     *         description: Lista de tiposOperacionesVehiculos
     *       500:
     *         description: Error interno
     */
    /**
     * @swagger
     * /Constantes/consultarRelevanciasMercaderias:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta relevanciasMercaderias
     *     responses:
     *       200:
     *         description: Lista de relevanciasMercaderias
     *       500:
     *         description: Error interno
     */
    /**
     * @swagger
     * /Constantes/consultarCondicionesNegociaciones:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta condicionesNegociaciones
     *     responses:
     *       200:
     *         description: Lista de condicionesNegociaciones
     *       500:
     *         description: Error interno
     */
    /**
     * @swagger
     * /Constantes/consultarTasasIsc:
     *   get:
     *     tags: [ConstantesGetSimples]
     *     summary: Consulta tasasIsc
     *     responses:
     *       200:
     *         description: Lista de tasasIsc
     *       500:
     *         description: Error interno
    */
    router.get(path, constantesHandler(prop));
}
// Endpoints con l�gica adicional
/**
 * @swagger
 * /Constantes/consultarCaracteristicasCargas:
 *   get:
 *     tags: [Constantes]
 *     summary: consultar Caracteristicas Cargas
 *     responses:
 *       200:
 *         description: Lista de pa�ses
 *       500:
 *         description: Error interno
 */
/**
 * @swagger
 * /Constantes/consultarCaracteristicasCargas:
 *   get:
 *     tags: [Constantes]
 *     summary: consultar Caracteristicas Cargas
 *     responses:
 *       200:
 *         description: Lista de pa�ses
 *       500:
 *         description: Error interno
 */
router.get('/consultarPaises', async (req, res) => {
    try {
        const result = await DE.consultarPaises();
        sendConstantes(res, result);
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
/**
 * @swagger
 * /Constantes/consultarDepartamentos:
 *   get:
 *     tags: [Constantes]
 *     summary: Consulta lista de departamentos
 *     responses:
 *       200:
 *         description: Lista de departamentos
 *       500:
 *         description: Error interno
 */
router.get('/consultarDepartamentos', async (req, res) => {
    try {
        const result = await DE.consultarDepartamentos();
        sendConstantes(res, result);
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
/**
 * @swagger
 * /Constantes/consultarDistritos:
 *   get:
 *     tags: [Constantes]
 *     summary: Consulta lista de distritos por departamento
 *     parameters:
 *       - in: query
 *         name: departamentoId
 *         required: true
 *         schema:
 *           type: integer
 *         description: ID del departamento
 *     responses:
 *       200:
 *         description: Lista de distritos
 *       400:
 *         description: Par�metro departamentoId inv�lido
 *       500:
 *         description: Error interno
 */
router.get('/consultarDistritos', async (req, res) => {
    const departamentoId = Number(req.query.departamentoId);
    if (isNaN(departamentoId)) {
        return handleError(res, 400, 'departamentoId es requerido y debe ser un n�mero');
    }
    try {
        const result = await DE.consultarDistritos(departamentoId);
        sendConstantes(res, result);
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
/**
  * @swagger
  * /Constantes/consultarTiposRegimenes:
  *   get:
  *     tags: [Constantes]
  *     summary: Consulta lista de tipos de reg�menes
  *     responses:
  *       200:
  *         description: Lista de tipos de reg�menes
  *       500:
  *         description: Error interno
  */
router.get('/consultarTiposRegimenes', async (req, res) => {
    try {
        const result = await DE.consultarTiposRegimenes();
        sendConstantes(res, result);
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
/**
 * @swagger
 * /Constantes/consultarCiudades:
 *   get:
 *     tags: [Constantes]
 *     summary: Consulta lista de ciudades por distrito
 *     parameters:
 *       - in: query
 *         name: distritoId
 *         required: true
 *         schema:
 *           type: integer
 *         description: ID del distrito
 *     responses:
 *       200:
 *         description: Lista de ciudades
 *       400:
 *         description: Par�metro distritoId inv�lido
 *       500:
 *         description: Error interno
 */
router.get('/consultarCiudades', async (req, res) => {
    const distritoId = Number(req.query.distritoId);
    if (isNaN(distritoId)) {
        return handleError(res, 400, 'distritoId es requerido y debe ser un n�mero');
    }
    try {
        const result = await DE.consultarCiudades(distritoId);
        sendConstantes(res, result);
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
export default (app) => {
    app.use('/Constantes', router);
};
//# sourceMappingURL=constantesRoute.js.map