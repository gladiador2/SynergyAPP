import { Router, type Request, type Response } from 'express';
import apiset from 'facturacionelectronicapy-setapi';
import { handleError } from './utils.js';

const router = Router();
const setapi = apiset.default;

const endpoints = [
    {
        path: '/consulta',
        handler: async (req: Request) =>
            setapi.consulta(req.body.id, req.body.cdc, req.body.env, req.body.cert, req.body.key, req.body.config)
    },
    {
        path: '/consultaRUC',
        handler: async (req: Request) =>
            setapi.consultaRUC(req.body.id, req.body.ruc, req.body.env, req.body.cert, req.body.key, req.body.config)
    },
    {
        path: '/consultaLote',
        handler: async (req: Request) =>
            setapi.consultaLote(req.body.id, req.body.numeroLote, req.body.env, req.body.cert, req.body.key, req.body.config)
    },
    {
        path: '/recibe',
        handler: async (req: Request) =>
            setapi.recibe(req.body.id, req.body.xml, req.body.env, req.body.cert, req.body.key, req.body.config)
    },
    {
        path: '/recibeLote',
        handler: async (req: Request) =>
            setapi.recibeLote(req.body.id, req.body.xml, req.body.env, req.body.cert, req.body.key, req.body.config)
    },
    {
        path: '/evento',
        handler: async (req: Request) =>
            setapi.evento(req.body.id, req.body.xml, req.body.env, req.body.cert, req.body.key, req.body.config)
    }
];

for (const { path, handler } of endpoints) {
    router.post(path, async (req: Request, res: Response) => {
        try {
            const result = await handler(req);
            res.json({ success: true, data: result });
        } catch (error) {
            handleError(res, 500, error);
        }
    });
}

export default router;
