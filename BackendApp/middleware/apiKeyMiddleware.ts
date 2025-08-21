import type { Request, Response, NextFunction } from 'express';

// Firma de índice para clientes
interface Cliente {
    id: number;
    nombre: string;
}
const clientes: { [key: string]: Cliente } = {
    'apikey123': { id: 1, nombre: 'Cliente A' },
    'apikey456': { id: 2, nombre: 'Cliente B' }
};

// Extiende el tipo Request para incluir 'cliente'
interface RequestConCliente extends Request {
    cliente?: Cliente;
}

export default function apiKeyMiddleware(req: RequestConCliente, res: Response, next: NextFunction) {
    const apiKey = req.headers['x-api-key'];
    if (!apiKey || typeof apiKey !== 'string' || !clientes[apiKey]) {
        return res.status(401).json({ success: false, error: 'API key inválido' });
    }
    req.cliente = clientes[apiKey]; // Adjunta el cliente al request
    console.log(`API Key validada para cliente: ${req.cliente.nombre} (ID: ${req.cliente.id})`);
    next();
}

