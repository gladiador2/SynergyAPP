import { Router, type Request, type Response } from 'express';
import xmlgen from 'facturacionelectronicapy-xmlgen';
import apiset from 'facturacionelectronicapy-setapi'
import xmlsign from 'facturacionelectronicapy-xmlsign'
import kude from 'facturacionelectronicapy-kude'
import qr from 'facturacionelectronicapy-qrgen'
import pool from '../db.js';
import jwt from 'jsonwebtoken';
import { parseStringPromise } from "xml2js";



const Kude = kude.default;
const qrgen = qr.default;
const DE = xmlgen.default; // Acceso al objeto con los m�todos
const DESign = xmlsign.default;
const router = Router();

const TIPOS_DOCUMENTOS: Record<number, string> = {
    1: 'Factura electrónica',
    2: 'Factura electrónica de exportación',
    3: 'Factura electrónica de importación',
    4: 'Autofactura electrónica',
    5: 'Nota de crédito electrónica',
    6: 'Nota de débito electrónica',
    7: 'Nota de remisión electrónica',
    8: 'Comprobante de retención electrónico',
};

function extractDocumentoData(body: unknown): { documento: string | null; tipoDe: string | null } {
    if (!body || typeof body !== 'object') {
        return { documento: null, tipoDe: null };
    }

    const payload = body as {
        data?: {
            tipoDocumento?: unknown;
            establecimiento?: unknown;
            punto?: unknown;
            numero?: unknown;
        };
    };

    const data = payload.data;
    if (!data || typeof data !== 'object') {
        return { documento: null, tipoDe: null };
    }

    const establecimiento = typeof data.establecimiento === 'string' ? data.establecimiento : null;
    const punto = typeof data.punto === 'string' ? data.punto : null;
    const numero = typeof data.numero === 'string' ? data.numero : null;
    const documento = establecimiento && punto && numero ? `${establecimiento}-${punto}-${numero}` : null;

    const tipoDocumento = typeof data.tipoDocumento === 'number' ? data.tipoDocumento : null;
    const tipoDe = tipoDocumento !== null ? TIPOS_DOCUMENTOS[tipoDocumento] ?? null : null;

    return { documento, tipoDe };
}

/**
 * @swagger
 * /facturacion/getDepartamento:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Obtiene un departamento por ID
 *     description: Retorna la informaci�n de un departamento seg�n su ID.
 *     security:
 *       - ApiKeyAuth: []
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

router.post('/getDepartamento', (req: Request, res: Response) => {
    const { departamentoId } = req.body;
    if (typeof departamentoId !== 'number') {
        return res.status(400).json({ error: 'id departamento no es un numero' });
    }
    const departamento = DE.getDepartamento(departamentoId);
    if (departamento) {
        res.json({ success: true, data: departamento });
    } else {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/getDistrito', (req: Request, res: Response) => {
    const { distritoId } = req.body;
    if (typeof distritoId !== 'number') {
        return res.status(400).json({ error: 'id distrito no es un numero' });
    }
    const distrito = DE.getDistrito(distritoId);
    if (distrito) {
        res.json({ success: true, data: distrito });
    } else {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/getCiudad', (req: Request, res: Response) => {
    const { ciudadId } = req.body;
    if (typeof ciudadId !== 'number') {
        return res.status(400).json({ error: 'id ciudad no es un numero' });
    }
    const ciudad = DE.getCiudad(ciudadId);
    if (ciudad) {
        res.json({ success: true, data: ciudad });
    } else {
        res.status(404).json({ success: false, message: 'Error al obtener' });
    }
});

/**
 * @swagger
 * /facturacion/setapi/consultaRUC:
 *   post:
 *     tags:
 *       - Facturacion
 *     summary: Consulta por RUC
 *     security:
 *       - ApiKeyAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               id: { type: number }
 *               ruc: { type: string }
 *               
 *               config: { type: object }
 *           example:
 *             id: 1
 *             ruc: "3563476"
 *             config: {}
 *     responses:
 *       200: { description: Respuesta exitosa }
 *       500: { description: Error interno }
 */
router.post('/setapi/consultaRUC', async (req: Request, res: Response) => {
    const { id, ruc, config } = req.body;
    try {
        const certData = process.env.Certificado_p12;
        const clave = process.env.Clave;
        const env = process.env.Ambiente;

        if (!certData || !clave || !env) {
            return res.status(500).json({ success: false, error: 'Variables de entorno incompletas' });
        }

        if (env !== 'test' && env !== 'prod') {
            return res.status(500).json({ success: false, error: 'Ambiente inválido' });
        }

        const result = await apiset.default.consultaRUC(id, ruc, env, certData, clave, config);
        const data = typeof result === 'string' ? await parseStringPromise(result, { explicitArray: false }) : result;
        res.json({ success: true, data });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
function normalizeXML(xml: string): string {
            xml = xml.split('\r\n').join('');
            xml = xml.split('\n').join('');
            xml = xml.split('\t').join('');
            xml = xml.split('    ').join('');
            xml = xml.split('>    <').join('><');
            xml = xml.split('>  <').join('><');
            xml = xml.replace(/\r?\n|\r/g, '');
            return xml;
        }
/**
 * @swagger
 * /facturacion/generateXMLDE:
 *   post:
 *     tags:
 *        - Facturacion
 *     summary: Genera XML DE
 *     description: Genera el archivo XML del Documento Electr�nico exigido por SIFEN en base a JSON. Requiere los objetos 'params' y 'data' seg�n la estructura oficial.
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
router.post('/generateXMLDE', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    let estado = 'success';
    let errorMsg = null;
    let jsonId: number | null = null;
    
    let xmlId: number | null = null;
    let xml: string = '';
    let normalizedXml: string = '';
    let signedXml: string = '';
    let xmlResult = '';
    const idCSC = process.env.idCSC;
    const CSC = process.env.CSC;
    const env = process.env.Ambiente;
    const certData = process.env.Certificado_p12;
    const clave = process.env.Clave;
    if (!certData || !clave) {
        return res.status(500).json({ success: false, error: 'Variables de entorno de certificado incompletas' });
    }
    // Extraer el usuario del token JWT sin validar
    //const authHeader = req.headers['authorization'];
    //let usuarioID: number | null = null;
    //let token: string | undefined = undefined;

    //if (authHeader && authHeader.startsWith('Bearer ')) {
    //    const parts = authHeader.split(' ');
    //    token = parts.length === 2 ? parts[1] : undefined;
    //}

    //if (token) {
    //    try {
    //        // Solo decodifica el token, no valida la firma ni expiraci�n
    //        const decoded: any = jwt.decode(token);
    //        usuarioID = decoded?.id ?? null;
    //    } catch (err) {
    //        usuarioID = null;
    //    }
    //}


    // Guardar el JSON recibido
    const { documento, tipoDe } = extractDocumentoData(req.body);

    try {
        const result = await pool.query(
            `INSERT INTO json_recibido (datos_json, estado, documento, tipo_de, fecha_creacion, usuarioID) VALUES ($1, $2, $3, $4, NOW(), $5) RETURNING id`,
            //[req.body, estado, usuarioID]
            [req.body, estado, documento, tipoDe, 1]
        );
        jsonId = result.rows[0].id;
    } catch (err) {
        estado = 'error';
        errorMsg = err instanceof Error ? err.message : String(err);
        await pool.query(
            `INSERT INTO json_recibido (datos_json, estado, error, documento, tipo_de, fecha_creacion, usuarioID) VALUES ($1, $2, $3, $4, $5, NOW(), $6)`,
            //[req.body, estado, usuarioID]
            [req.body, estado, errorMsg, documento, tipoDe, 1]
        );
        return res.status(500).json({ success: false, error: errorMsg });
    }

    // Generar el XML
    try {
        xml = await DE.generateXMLDE(params, data, config);
        normalizedXml = normalizeXML(xml);

        // Guardar el XML en la tabla xml_generado y relacionar con jsonId
        const xmlResult = await pool.query(
            `INSERT INTO xml_generado (datos_xml, json_id, fecha_creacion) VALUES ($1, $2, NOW()) RETURNING id`,
            [normalizedXml, jsonId]
        );
        xmlId = xmlResult.rows[0]?.id ?? null;
    } catch (error) {
        estado = 'error';
        errorMsg = error instanceof Error ? error.message : String(error);
        if (jsonId) {
            await pool.query(
                `UPDATE json_recibido SET estado = $1, error = $2 WHERE id = $3`,
                [estado, errorMsg, jsonId]
            );
        }
        return res.status(500).json({ success: false, error: errorMsg, jsonId });
    }
    // Firmar el XML
    try {
        if (!normalizedXml) {
            return res.status(500).json({ success: false, error: 'XML no generado', jsonId });
        }
        
        signedXml = await DESign.signXML(normalizedXml, certData, clave, true);

        // Guardar el XML firmado en la tabla xml_firmado y relacionar con xml_generado_id
        await pool.query(
            `INSERT INTO xml_firmado (xml_generado_id, datos_xml_firmado, estado, error, respuesta, fecha_creacion) VALUES ($1, $2, $3, $4, $5, NOW())`,
            [xmlId, signedXml, 'success', null, null]
        );
    } catch (error) {
        // Si ocurre un error al firmar, actualiza el estado del JSON a 'error' y guarda el mensaje de error
        estado = 'error';
        errorMsg = error instanceof Error ? error.message : String(error);

        await pool.query(
            `INSERT INTO xml_firmado (xml_generado_id, datos_xml_firmado, estado, error, respuesta, fecha_creacion) VALUES ($1, $2, $3, $4, $5, NOW())`,
            [xmlId, null, 'error', errorMsg, null]
        );
        return res.status(500).json({ success: false, error: errorMsg, jsonId });
    }
    

    if (!idCSC || !CSC || !env) {
        return res.status(500).json({ success: false, error: 'Variables de entorno de QR incompletas' });
    }

    if (env !== 'test' && env !== 'prod') {
        return res.status(500).json({ success: false, error: 'Ambiente inválido' });
    }    
        // genrar QR
    try { 
        

        const result = await qrgen.generateQR(signedXml, idCSC, CSC, env);
        xmlResult = normalizeXML(result);
       
    }
    catch (error) {
        const errorMsg = error instanceof Error ? error.message : String(error);
        return res.status(500).json({ success: false, error: errorMsg });
    } 
    //Enviar el XML a SIFEN usando SET API
   /* try {
        if (jsonId === null) {
            return res.status(500).json({ success: false, error: 'jsonId no generado' });
        }

        const result = await apiset.default.recibe(jsonId, xmlResult, env, certData, clave, config);
        const data = typeof result === 'string' ? await parseStringPromise(result, { explicitArray: false }) : result;
        res.json({ success: true, data });
    } catch (error) {
        const errorMsg = error instanceof Error ? error.message : String(error);
        res.status(500).json({ success: false, error: errorMsg });
    }
        */
       //generar kude utilizando kude
         try {
                const urlLogo = process.env.logo;
                    const java8Path = process.env.java8path;
                    const srcJasper = process.env.srcJasper;
                    const destFolder = process.env.destFolder;
                    const ambiente = process.env.AMBIENTE;
                    const xml = process.env.xmlSigned;

            if (!java8Path || !urlLogo || !ambiente || !srcJasper || !destFolder || !xml) {
                return res.status(500).json({ success: false, error: 'Variables de entorno de KUDE incompletas' });
            }

            const normalizedUrlLogo = urlLogo.trim();
            if (/\s/.test(normalizedUrlLogo)) {
                return res.status(400).json({
                    success: false,
                    error: "La ruta 'logo' contiene espacios. Usa una ruta sin espacios o una URL sin espacios."
                });
            }

            const normalizedSrcJasper = /[\\\/]$/.test(srcJasper) ? srcJasper : `${srcJasper}\\`;

            const jsonParm = `{"logo":"${normalizedUrlLogo}"}`;

            const kudeResult = await Kude.generateKUDE(java8Path, xml, normalizedSrcJasper, destFolder, jsonParm);
            res.json({ success: true, xml: signedXml, kude: kudeResult });
        } catch (error) {
            const errorMsg = error instanceof Error ? error.message : String(error);
            res.status(500).json({ success: false, error: errorMsg });
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
router.post('/generateXMLEventoCancelacion', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoCancelacion(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoInutilizacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Inutilizaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoInutilizacion', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoInutilizacion(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoConformidad:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Conformidad
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoConformidad', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoConformidad(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoDisconformidad:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Disconformidad
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoDisconformidad', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoDisconformidad(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoDesconocimiento:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Desconocimiento
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoDesconocimiento', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoDesconocimiento(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoNotificacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Notificaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoNotificacion', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoNotificacion(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoNominacion:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Nominaci�n
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoNominacion', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoNominacion(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/generateXMLEventoActualizacionDatosTransporte:
 *   post:
 *     tags:
 *       - xmlgen
 *     summary: Genera XML Evento Actualizaci�n Datos Transporte
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
 *       200:
 *         description: XML generado
 *       500:
 *         description: Error interno
 */
router.post('/generateXMLEventoActualizacionDatosTransporte', async (req: Request, res: Response) => {
    const { params, data, config } = req.body;
    try {
        const xml = await DE.generateXMLEventoActualizacionDatosTransporte(params, data, config);
        res.json({ success: true, xml });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});


/**
 * @swagger
 * /facturacion/setapi/consulta:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Consulta documento por CDC
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/setapi/consulta', async (req: Request, res: Response) => {
    const { id, cdc, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.consulta(id, cdc, env, cert, key, config);
        res.json({ success: true, data: result });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});






/**
 * @swagger
 * /facturacion/setapi/consultaLote:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Consulta por n�mero de lote
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/setapi/consultaLote', async (req: Request, res: Response) => {
    const { id, numeroLote, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.consultaLote(id, numeroLote, env, cert, key, config);
        res.json({ success: true, data: result });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/setapi/recibe:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Recibe documento XML
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/setapi/recibe', async (req: Request, res: Response) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.recibe(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/setapi/recibeLote:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Recibe lote de documentos XML
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/setapi/recibeLote', async (req: Request, res: Response) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.recibeLote(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

/**
 * @swagger
 * /facturacion/setapi/evento:
 *   post:
 *     tags:
 *       - setapi
 *     summary: Env�a evento XML
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/setapi/evento', async (req: Request, res: Response) => {
    const { id, xml, env, cert, key, config } = req.body;
    try {
        const result = await apiset.default.evento(id, xml, env, cert, key, config);
        res.json({ success: true, data: result });
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});
/**
 * @swagger
 * /facturacion/xmlsign/signXML:
 *   post:
 *     tags:
 *       - xmlsign
 *     summary: Firma un XML
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/xmlsign/signXML', async (req: Request, res: Response) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXML(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/xmlsign/signXMLFiles', async (req: Request, res: Response) => {
    const { xmls, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLFiles(xmls, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/xmlsign/signXMLEvento', async (req: Request, res: Response) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLEvento(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/xmlsign/signXMLRecibo', async (req: Request, res: Response) => {
    const { xml, file, password, signByNodeJS } = req.body;
    try {
        const result = await DESign.signXMLRecibo(xml, file, password, signByNodeJS);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/xmlsign/getExpiration', async (req: Request, res: Response) => {
    const { file, password, useNodeJS } = req.body;
    try {
        const result = await DESign.getExpiration(file, password, useNodeJS);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/kude/generateKUDE', async (req: Request, res: Response) => {
    const { java8Path, xmlSigned, urlLogo, ambiente } = req.body;
    try {
        const normalizedUrlLogo = typeof urlLogo === 'string' ? urlLogo.trim() : '';
        if (/\s/.test(normalizedUrlLogo)) {
            return res.status(400).json({
                success: false,
                error: "El parámetro 'urlLogo' contiene espacios. Usa una ruta/URL sin espacios."
            });
        }

        const jsonParm = "{\"logo\":\"empresa.png\"}";

        const result = await Kude.generateKUDE(java8Path, xmlSigned, normalizedUrlLogo, ambiente, jsonParm);
        res.json({ success: true, data: result });
    } catch (error) {
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
 *     security:
 *       - ApiKeyAuth: []
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
router.post('/qrgen/generateQR', async (req: Request, res: Response) => {
    const { xmlSigned, idCSC, CSC, env } = req.body;
    try {
        const result = await qrgen.generateQR(xmlSigned, idCSC, CSC, env);
        res.json({ success: true, data: result });
    } catch (error) {
        // Si es un Error, env�a el mensaje
        const errorMessage = error instanceof Error ? error.message : error;
        res.status(500).json({ success: false, error: errorMessage });
    }
});

import setapiRoutes from './setapiRoutes.js';
import xmlsignRoutes from './xmlsignRoutes.js';
import kudeRoutes from './kudeRoutes.js';
import qrgenRoutes from './qrgenRoutes.js';

// ...otros imports globales si son necesarios...

router.use('/setapi', setapiRoutes);
router.use('/xmlsign', xmlsignRoutes);
router.use('/kude', kudeRoutes);
router.use('/qrgen', qrgenRoutes);

export default router;
