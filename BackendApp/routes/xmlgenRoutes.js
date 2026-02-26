import { Router } from 'express';
import xmlgen from 'facturacionelectronicapy-xmlgen';
import pool from '../db.js';
import jwt from 'jsonwebtoken';
import { handleError, validateNumberParam } from './utils.js';
const DE = xmlgen.default;
const router = Router();
// Auxiliar para endpoints de consulta por ID
function handleGetById(req, res, paramName, getter) {
    const id = req.body[paramName];
    if (!validateNumberParam(id)) {
        return res.status(400).json({ error: `id ${paramName} no es un numero` });
    }
    const result = getter(id);
    if (result) {
        res.json({ success: true, data: result });
    }
    else {
        res.status(404).json({ success: false, message: 'Error al obtener' });
    }
}
// Departamento
router.post('/getDepartamento', (req, res) => handleGetById(req, res, 'departamentoId', DE.getDepartamento));
// Distrito
router.post('/getDistrito', (req, res) => handleGetById(req, res, 'distritoId', DE.getDistrito));
// Ciudad
router.post('/getCiudad', (req, res) => handleGetById(req, res, 'ciudadId', DE.getCiudad));
// Generaci�n de XML DE
router.post('/generateXMLDE', async (req, res) => {
    const { params, data, config } = req.body;
    let estado = 'success';
    let errorMsg = null;
    let jsonId = null;
    //// Extraer usuario del token JWT
    //const authHeader = req.headers['authorization'];
    //let usuarioID: number | null = null;
    //let token: string | undefined = undefined;
    //if (authHeader?.startsWith('Bearer ')) {
    //    token = authHeader.split(' ')[1];
    //}
    //if (token) {
    //    try {
    //        const decoded: any = jwt.decode(token);
    //        usuarioID = decoded?.id ?? null;
    //    } catch {
    //        usuarioID = null;
    //    }
    //}
    try {
        const result = await pool.query(`INSERT INTO json_recibido (datos_json, estado, fecha_creacion, usuarioID) VALUES ($1, $2, NOW(), $3) RETURNING id`, [req.body, estado, 1]);
        jsonId = result.rows[0].id;
    }
    catch (err) {
        estado = 'error';
        errorMsg = err instanceof Error ? err.message : String(err);
        await pool.query(`INSERT INTO json_recibido (datos_json, estado, error, fecha_creacion, usuarioID) VALUES ($1, $2, $3, NOW(), $4)`, [req.body, estado, errorMsg, 1]);
        return handleError(res, 500, errorMsg);
    }
    try {
        const xml = await DE.generateXMLDE(params, data, config);
        await pool.query(`INSERT INTO xml_generado (datos_xml, json_id, fecha_creacion) VALUES ($1, $2, NOW())`, [xml, jsonId]);
        res.json({ success: true, xml, jsonId });
    }
    catch (error) {
        estado = 'error';
        errorMsg = error instanceof Error ? error.message : String(error);
        if (jsonId) {
            await pool.query(`UPDATE json_recibido SET estado = $1, error = $2 WHERE id = $3`, [estado, errorMsg, jsonId]);
        }
        handleError(res, 500, errorMsg, { jsonId });
    }
});
// Eventos XMLGEN
const eventEndpoints = [
    { path: '/generateXMLEventoCancelacion', method: DE.generateXMLEventoCancelacion },
    { path: '/generateXMLEventoInutilizacion', method: DE.generateXMLEventoInutilizacion },
    { path: '/generateXMLEventoConformidad', method: DE.generateXMLEventoConformidad },
    { path: '/generateXMLEventoDisconformidad', method: DE.generateXMLEventoDisconformidad },
    { path: '/generateXMLEventoDesconocimiento', method: DE.generateXMLEventoDesconocimiento },
    { path: '/generateXMLEventoNotificacion', method: DE.generateXMLEventoNotificacion },
    { path: '/generateXMLEventoNominacion', method: DE.generateXMLEventoNominacion },
    { path: '/generateXMLEventoActualizacionDatosTransporte', method: DE.generateXMLEventoActualizacionDatosTransporte }
];
for (const { path, method } of eventEndpoints) {
    router.post(path, async (req, res) => {
        const { params, data, config } = req.body;
        try {
            const xml = await method(params, data, config);
            res.json({ success: true, xml });
        }
        catch (error) {
            handleError(res, 500, error);
        }
    });
}
export default router;
//# sourceMappingURL=xmlgenRoutes.js.map