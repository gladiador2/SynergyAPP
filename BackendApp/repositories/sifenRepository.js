import pool from '../db.js';
const TIPOS_DOCUMENTOS = {
    1: 'Factura electrónica',
    2: 'Factura electrónica de exportación',
    3: 'Factura electrónica de importación',
    4: 'Autofactura electrónica',
    5: 'Nota de crédito electrónica',
    6: 'Nota de débito electrónica',
    7: 'Nota de remisión electrónica',
    8: 'Comprobante de retención electrónico',
};
function extractDocumentoData(datosJson) {
    if (!datosJson || typeof datosJson !== 'object') {
        return { documento: null, tipoDe: null };
    }
    const payload = datosJson;
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
export async function insertJsonRecibido(datosJson, estado, usuarioId) {
    const { documento, tipoDe } = extractDocumentoData(datosJson);
    const result = await pool.query(`INSERT INTO json_recibido (datos_json, estado, documento, tipo_de, fecha_creacion, usuarioID) VALUES ($1, $2, $3, $4, NOW(), $5) RETURNING id`, [datosJson, estado, documento, tipoDe, usuarioId]);
    return result.rows[0].id;
}
export async function insertJsonRecibidoError(datosJson, estado, error, usuarioId) {
    const { documento, tipoDe } = extractDocumentoData(datosJson);
    await pool.query(`INSERT INTO json_recibido (datos_json, estado, error, documento, tipo_de, fecha_creacion, usuarioID) VALUES ($1, $2, $3, $4, $5, NOW(), $6)`, [datosJson, estado, error, documento, tipoDe, usuarioId]);
}
export async function updateJsonRecibidoError(id, estado, error) {
    await pool.query(`UPDATE json_recibido SET estado = $1, error = $2 WHERE id = $3`, [estado, error, id]);
}
export async function updateJsonRecibidoEstado(id, estado, error = null) {
    await pool.query(`UPDATE json_recibido SET estado = $1, error = $2 WHERE id = $3`, [estado, error, id]);
}
export async function getJsonRecibidoStatusById(id) {
    const result = await pool.query(`SELECT id, estado, error, fecha_creacion
         FROM json_recibido
         WHERE id = $1`, [id]);
    if (!result.rows[0]) {
        return null;
    }
    return {
        id: Number(result.rows[0].id),
        estado: String(result.rows[0].estado),
        error: result.rows[0].error ?? null,
        fechaCreacion: new Date(result.rows[0].fecha_creacion),
    };
}
export async function insertXmlGenerado(datosXml, jsonId) {
    const result = await pool.query(`INSERT INTO xml_generado (datos_xml, json_id, fecha_creacion) VALUES ($1, $2, NOW()) RETURNING id`, [datosXml, jsonId]);
    return result.rows[0]?.id ?? 0;
}
export async function insertXmlFirmado(input) {
    await pool.query(`INSERT INTO xml_firmado (xml_generado_id, datos_xml_firmado, estado, error, respuesta, fecha_creacion) VALUES ($1, $2, $3, $4, $5, NOW())`, [input.xmlGeneradoId, input.datosXmlFirmado, input.estado, input.error, input.respuesta]);
}
export async function updateXmlFirmadoRespuestaByXmlGeneradoId(xmlGeneradoId, estado, respuesta, error) {
    await pool.query(`UPDATE xml_firmado
         SET estado = $1,
             respuesta = $2,
             error = $3
         WHERE xml_generado_id = $4`, [estado, respuesta, error, xmlGeneradoId]);
}
//# sourceMappingURL=sifenRepository.js.map