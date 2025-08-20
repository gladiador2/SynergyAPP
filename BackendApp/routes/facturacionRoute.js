import { Router } from 'express';
import xmlgen from 'facturacionelectronicapy-xmlgen';
import apiset from 'facturacionelectronicapy-setapi';
import xmlsign from 'facturacionelectronicapy-xmlsign';
import kude from 'facturacionelectronicapy-kude';
import qr from 'facturacionelectronicapy-qrgen';
const Kude = kude.default;
const qrgen = qr.default;
const DE = xmlgen.default; // Acceso al objeto con los m�todos
const DESign = xmlsign.default;
const router = Router();
/**
 * @swagger
 * /facturacion/getDepartamento:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Obtiene un departamento por ID
 *     description: Retorna la informaci�n de un departamento seg�n su ID.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               departamentoId:
 *                 type: number
 *                 example: 11
 *           example:
 *             departamentoId: 11
 *     responses:
 *       200:
 *         description: Departamento encontrado
 *       400:
 *         description: ID inv�lido
 *       404:
 *         description: No encontrado
 */
router.post('/getDepartamento', (req, res) => {
    const { departamentoId } = req.body;
    if (typeof departamentoId !== 'number') {
        return res.status(400).json({ error: 'id departamento no es un numero' });
    }
    const departamento = DE.getDepartamento(departamentoId);
    if (departamento) {
        res.json({ success: true, data: departamento });
    }
    else {
        res.status(404).json({ success: false, message: 'Error al obtener' });
    }
});
/**
 * @swagger
 * /facturacion/getDistrito:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Obtiene un distrito por ID
 *     description: Retorna la informaci�n de un distrito seg�n su ID.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               distritoId:
 *                 type: number
 *                 example: 143
 *     responses:
 *       200:
 *         description: Distrito encontrado
 *       400:
 *         description: ID inv�lido
 *       404:
 *         description: No encontrado
 */
router.post('/getDistrito', (req, res) => {
    const { distritoId } = req.body;
    if (typeof distritoId !== 'number') {
        return res.status(400).json({ error: 'id distrito no es un numero' });
    }
    const distrito = DE.getDistrito(distritoId);
    if (distrito) {
        res.json({ success: true, data: distrito });
    }
    else {
        res.status(404).json({ success: false, message: 'Error al obtener' });
    }
});
/**
 * @swagger
 * /facturacion/getCiudad:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Obtiene una ciudad por ID
 *     description: Retorna la informaci�n de una ciudad seg�n su ID.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               ciudadId:
 *                 type: number
 *                 example: 3344
 *     responses:
 *       200:
 *         description: Ciudad encontrada
 *       400:
 *         description: ID inv�lido
 *       404:
 *         description: No encontrado
 */
router.post('/getCiudad', (req, res) => {
    const { ciudadId } = req.body;
    if (typeof ciudadId !== 'number') {
        return res.status(400).json({ error: 'id ciudad no es un numero' });
    }
    const ciudad = DE.getCiudad(ciudadId);
    if (ciudad) {
        res.json({ success: true, data: ciudad });
    }
    else {
        res.status(404).json({ success: false, message: 'Error al obtener' });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLDE:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML DE
 *     description: Genera el archivo XML del Documento Electr�nico exigido por SIFEN en base a JSON. Requiere los objetos 'params' y 'data' seg�n la estructura oficial.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               params:
 *                 type: object
 *                 description: Datos est�ticos del contribuyente emisor.
 *               data:
 *                 type: object
 *                 description: Datos variables para cada documento electr�nico.
 *               config:
 *                 type: object
 *                 description: Opciones adicionales (opcional).
 *           example:
 *             params:
 *               version: "150"
 *               ruc: "80000001-5"
 *               razonSocial: "EMPRESA S.A."
 *               nombreFantasia: "EMPRESA"
 *               tipoContribuyente: 1
 *               tipoRegimen: 1
 *               timbradoNumero: "12345678"
 *               timbradoFecha: "2025-01-01"
 *               establecimientos:
 *                 - codigo: "001"
 *                   direccion: "Av. Principal 123"
 *                   numeroCasa: "123"
 *                   complementoDireccion1: ""
 *                   complementoDireccion2: ""
 *                   departamento: 1
 *                   distrito: 1
 *                   ciudad: 1
 *                   telefono: "021123456"
 *                   email: "info@empresa.com"
 *                   denominacion: "Casa Central"
 *               actividadesEconomicas:
 *                 - codigo: "123"
 *                   descripcion: "Comercio al por mayor"
 *             data:
 *               tipoDocumento: 1
 *               establecimiento: "001"
 *               punto: "001"
 *               numero: "1000050"
 *               fecha: "2025-08-19T10:00:00"
 *               tipoEmision: 1
 *               tipoTransaccion: 1
 *               tipoImpuesto: 1
 *               moneda: "PYG"
 *               cliente:
 *                 contribuyente: true
 *                 tipoContribuyente: 1
 *                 ruc: "80000002-7"
 *                 razonSocial: "CLIENTE S.A."
 *                 nombreFantasia: "CLIENTE"
 *                 direccion: "Calle 1"
 *                 numeroCasa: "456"
 *                 departamento: 1
 *                 distrito: 1
 *                 ciudad: 1
 *                 pais: "PRY"
 *                 tipoOperacion: 1
 *                 telefono: "021654321"
 *                 email: "contacto@cliente.com"
 *                 codigo: "C001"
 *               items:
 *                 - descripcion: "Producto Gravado"
 *                   cantidad: 1
 *                   precioUnitario: 10000
 *                   unidadMedida: 77
 *                   ivaTipo: 3
 *                   ivaBase: 0
 *                   iva: 0
 *               factura:
 *                 presencia: 1
 *                 fechaEnvio: "2025-08-19T10:05:00"
 *               condicion:
 *                 tipo: 1
 *                 entregas:
 *                   - tipo: 1
 *                     monto: 10000
 *                     moneda: "PYG"
 *             config:
 *               defaultValues: true
 *               decimals: 2
 *               taxDecimals: 2
 *               pygDecimals: 0
 *               userObjectRemove: false
 *
 *     responses:
 *       200:
 *         description: XML generado exitosamente
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 xml:
 *                   type: string
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLDE', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLDE(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoCancelacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Cancelaci�n
 *     description: Genera el XML para el evento de cancelaci�n de un documento electr�nico.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               params:
 *                 type: object
 *                 description: Datos est�ticos del contribuyente emisor.
 *                 example:
 *                   version: 150
 *                   ruc: "80069563-1"
 *                   razonSocial: "DE generado en ambiente de prueba - sin valor comercial ni fiscal"
 *                   nombreFantasia: "TIPS S.A. TECNOLOGIA Y SERVICIOS"
 *                   actividadesEconomicas:
 *                     - codigo: "1254"
 *                       descripcion: "Desarrollo de Software"
 *                   timbradoNumero: "12558946"
 *                   timbradoFecha: "2022-08-25"
 *                   tipoContribuyente: 2
 *                   tipoRegimen: 8
 *                   establecimientos:
 *                     - codigo: "001"
 *                       direccion: "Barrio Carolina"
 *                       numeroCasa: "0"
 *                       complementoDireccion1: "Entre calle 2"
 *                       complementoDireccion2: "y Calle 7"
 *                       departamento: 11
 *                       departamentoDescripcion: "ALTO PARANA"
 *                       distrito: 145
 *                       distritoDescripcion: "CIUDAD DEL ESTE"
 *                       ciudad: 3432
 *                       ciudadDescripcion: "PUERTO PTE.STROESSNER (MUNIC)"
 *                       telefono: "0973-527155"
 *                       email: "tips@tips.com.py, tips@gmail.com"
 *                       denominacion: "Sucursal 1"
 *               data:
 *                 type: object
 *                 description: Datos variables para el evento de cancelaci�n.
 *                 example:
 *                   motivo: "Cancelaci�n por error"
 *                   fecha: "2022-08-14T10:11:00"
 *                   documentoReferencia: "001-001-0000001"
 *               config:
 *                 type: object
 *                 description: Opciones adicionales (opcional).
 *                 example: {}
 *     responses:
 *       200:
 *         description: XML generado exitosamente
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 xml:
 *                   type: string
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoCancelacion', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoCancelacion(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoInutilizacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Inutilizaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoInutilizacion', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoInutilizacion(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoConformidad:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Conformidad
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoConformidad', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoConformidad(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoDisconformidad:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Disconformidad
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoDisconformidad', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoDisconformidad(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoDesconocimiento:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Desconocimiento
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoDesconocimiento', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoDesconocimiento(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoNotificacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Notificaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoNotificacion', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoNotificacion(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoNominacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Nominaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoNominacion', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoNominacion(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/generateXMLEventoActualizacionDatosTransporte:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Actualizaci�n Datos Transporte
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoActualizacionDatosTransporte', async (req, res) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoActualizacionDatosTransporte(params, data, config);
        res.json({ success: true, xml });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/consulta:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Consulta documento por CDC
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               cdc: { type: string }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             cdc: "CDC123"
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/consulta', async (req, res) => {
    const { id, cdc, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.consulta(id, cdc, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/consultaRUC:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Consulta por RUC
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               ruc: { type: string }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             ruc: "80000001-5"
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/consultaRUC', async (req, res) => {
    const { id, ruc, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.consultaRUC(id, ruc, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/consultaLote:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Consulta por n�mero de lote
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               numeroLote: { type: number }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             numeroLote: 12345
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/consultaLote', async (req, res) => {
    const { id, numeroLote, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.consultaLote(id, numeroLote, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/recibe:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Recibe documento XML
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               xml: { type: string }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             xml: "<xml>...</xml>"
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/recibe', async (req, res) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.recibe(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/recibeLote:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Recibe lote de documentos XML
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               xml: { type: array, items: { type: string } }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             xml: ["<xml>...</xml>", "<xml>...</xml>"]
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/recibeLote', async (req, res) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.recibeLote(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/setapi/evento:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Env�a evento XML
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               xml: { type: string }
 *               env: { type: string, enum: [test, prod] }
 *               cert: { type: string }
 *               key: { type: string }
 *               config: { type: object }
 *           example:
 *             id: 1
 *             xml: "<xml>...</xml>"
 *             env: "test"
 *             cert: "CERT_DATA"
 *             key: "KEY_DATA"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/evento', async (req, res) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.evento(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    }
    catch (error) {
        res.status(500).json({ success: false, error });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/signXML:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Firma un XML
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               xml: { type: string }
 *               file: { type: string }
 *               password: { type: string }
 *               signByNodeJS: { type: boolean }
 *           example:
 *             xml: "<xml>...</xml>"
 *             file: "CERT_DATA"
 *             password: "123456"
 *             signByNodeJS: true
 *     responses:
 *       200: { description: XML firmado }
 *       500: { description: Error interno }
 */
router.post('/xmlsign/signXML', async (req, res) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXML(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/signXMLFiles:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Firma m�ltiples XMLs
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               xmls: { type: array, items: { type: string } }
 *               file: { type: string }
 *               password: { type: string }
 *               signByNodeJS: { type: boolean }
 *           example:
 *             xmls: ["<xml>...</xml>", "<xml>...</xml>"]
 *             file: "CERT_DATA"
 *             password: "123456"
 *             signByNodeJS: true
 *     responses:
 *       200: { description: XMLs firmados }
 *       500: { description: Error interno }
 */
router.post('/xmlsign/signXMLFiles', async (req, res) => {
    const { xmls, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLFiles(xmls, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/signXMLEvento:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Firma un XML de evento
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               xml: { type: string }
 *               file: { type: string }
 *               password: { type: string }
 *               signByNodeJS: { type: boolean }
 *           example:
 *             xml: "<xml>...</xml>"
 *             file: "CERT_DATA"
 *             password: "123456"
 *             signByNodeJS: true
 *     responses:
 *       200: { description: XML de evento firmado }
 *       500: { description: Error interno }
 */
router.post('/xmlsign/signXMLEvento', async (req, res) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLEvento(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/signXMLRecibo:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Firma un XML de recibo
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               xml: { type: string }
 *               file: { type: string }
 *               password: { type: string }
 *               signByNodeJS: { type: boolean }
 *           example:
 *             xml: "<xml>...</xml>"
 *             file: "CERT_DATA"
 *             password: "123456"
 *             signByNodeJS: true
 *     responses:
 *       200: { description: XML de recibo firmado }
 *       500: { description: Error interno }
 */
router.post('/xmlsign/signXMLRecibo', async (req, res) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLRecibo(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/getExpiration:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Obtiene la fecha de expiraci�n del certificado
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               file: { type: string }
 *               password: { type: string }
 *               useNodeJS: { type: boolean }
 *           example:
 *             file: "CERT_DATA"
 *             password: "123456"
 *             useNodeJS: true
 *     responses:
 *       200: { description: Fecha de expiraci�n }
 *       500: { description: Error interno }
 */
router.post('/xmlsign/getExpiration', async (req, res) => {
    const { file, password, useNodeJS } = req.body;
    try {
        const result = await DESign.getExpiration(file, password, useNodeJS);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/kude/generateKUDE:
 *   post:
 *     tags:
 *       - kude
 *     summary: Genera el PDF KUDE a partir de un XML firmado
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               java8Path: { type: string, description: "Ruta al ejecutable de Java 8" }
 *               xmlSigned: { type: string, description: "XML firmado" }
 *               urlLogo: { type: string, description: "URL del logo a mostrar en el PDF" }
 *               ambiente: { type: string, description: "Ambiente de generaci�n (ej: test, prod)" }
 *           example:
 *             java8Path: "/usr/bin/java"
 *             xmlSigned: "<xml>...</xml>"
 *             urlLogo: "https://empresa.com/logo.png"
 *             ambiente: "test"
 *     responses:
 *       200: { description: PDF KUDE generado }
 *       500: { description: Error interno }
 */
router.post('/kude/generateKUDE', async (req, res) => {
    const { java8Path, xmlSigned, urlLogo, ambiente } = req.body;
    try {
        const result = await Kude.generateKUDE(java8Path, xmlSigned, urlLogo, ambiente);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/qrgen/generateQR:
 *   post:
 *     tags:
 *       - qrgen
 *     summary: Genera el QR a partir de un XML firmado
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               xmlSigned: { type: string, description: "XML firmado" }
 *               idCSC: { type: string, description: "Identificador del C�digo de Seguridad del Contribuyente" }
 *               CSC: { type: string, description: "C�digo de Seguridad del Contribuyente" }
 *               env: { type: string, enum: [test, prod], description: "Ambiente de generaci�n" }
 *           example:
 *             xmlSigned: "<xml>...</xml>"
 *             idCSC: "IDCSC123"
 *             CSC: "CSC123456"
 *             env: "test"
 *     responses:
 *       200: { description: QR generado }
 *       500: { description: Error interno }
 */
router.post('/qrgen/generateQR', async (req, res) => {
    const { xmlSigned, idCSC, CSC, env } = req.body;
    try {
        const result = await qrgen.generateQR(xmlSigned, idCSC, CSC, env);
        res.json({ success: true, data: result });
    }
    catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
export default (app) => {
    app.use('/facturacion', router);
};
//# sourceMappingURL=facturacionRoute.js.map