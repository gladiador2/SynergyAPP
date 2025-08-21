const clientes = {
    'apikey123': { id: 1, nombre: 'Cliente A' },
    'apikey456': { id: 2, nombre: 'Cliente B' }
};
export default function apiKeyMiddleware(req, res, next) {
    const apiKey = req.headers['x-api-key'];
    if (!apiKey || typeof apiKey !== 'string' || !clientes[apiKey]) {
        return res.status(401).json({ success: false, error: 'API key inv�lido' });
    }
    req.cliente = clientes[apiKey]; // Adjunta el cliente al request
    console.log(`API Key validada para cliente: ${req.cliente.nombre} (ID: ${req.cliente.id})`);
    next();
}
//# sourceMappingURL=apiKeyMiddleware.js.map