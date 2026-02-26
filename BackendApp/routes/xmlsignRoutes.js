import { Router } from 'express';
import xmlsign from 'facturacionelectronicapy-xmlsign';
import { handleError } from './utils.js';
const DESign = xmlsign.default;
const router = Router();
// Validaciones básicas para evitar caídas en librerías internas
function isString(val) {
    return typeof val === 'string' && val.length > 0;
}
function assertBody(req, checks) {
    for (const { key, type } of checks) {
        const v = req.body[key];
        if (type === 'string' && !isString(v)) {
            throw new Error(`Parámetro '${key}' es requerido y debe ser string`);
        }
        if (type === 'array' && !Array.isArray(v)) {
            throw new Error(`Parámetro '${key}' es requerido y debe ser array`);
        }
    }
}
const endpoints = [
    {
        path: '/signXML',
        handler: async (req) => {
            assertBody(req, [
                { key: 'xml', type: 'string' },
                { key: 'file', type: 'string' },
                { key: 'password', type: 'string' }
            ]);
            const signByNodeJS = req.body.signByNodeJS ?? true;
            return DESign.signXML(req.body.xml, req.body.file, req.body.password, signByNodeJS);
        }
    },
    {
        path: '/signXMLFiles',
        handler: async (req) => {
            assertBody(req, [
                { key: 'xmls', type: 'array' },
                { key: 'file', type: 'string' },
                { key: 'password', type: 'string' }
            ]);
            const signByNodeJS = req.body.signByNodeJS ?? true;
            return DESign.signXMLFiles(req.body.xmls, req.body.file, req.body.password, signByNodeJS);
        }
    },
    {
        path: '/signXMLEvento',
        handler: async (req) => {
            assertBody(req, [
                { key: 'xml', type: 'string' },
                { key: 'file', type: 'string' },
                { key: 'password', type: 'string' }
            ]);
            const signByNodeJS = req.body.signByNodeJS ?? true;
            return DESign.signXMLEvento(req.body.xml, req.body.file, req.body.password, signByNodeJS);
        }
    },
    {
        path: '/signXMLRecibo',
        handler: async (req) => {
            assertBody(req, [
                { key: 'xml', type: 'string' },
                { key: 'file', type: 'string' },
                { key: 'password', type: 'string' }
            ]);
            const signByNodeJS = req.body.signByNodeJS ?? true;
            return DESign.signXMLRecibo(req.body.xml, req.body.file, req.body.password, signByNodeJS);
        }
    },
    {
        path: '/getExpiration',
        handler: async (req) => {
            assertBody(req, [
                { key: 'file', type: 'string' },
                { key: 'password', type: 'string' }
            ]);
            const useNodeJS = req.body.useNodeJS ?? true;
            return DESign.getExpiration(req.body.file, req.body.password, useNodeJS);
        }
    }
];
for (const { path, handler } of endpoints) {
    router.post(path, async (req, res) => {
        try {
            const result = await handler(req);
            res.json({ success: true, data: result });
        }
        catch (error) {
            handleError(res, 500, error);
        }
    });
}
export default router;
//# sourceMappingURL=xmlsignRoutes.js.map