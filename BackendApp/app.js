import 'dotenv/config';
import express from 'express';
import helmet from 'helmet';
import rateLimit from 'express-rate-limit';
import { verificarToken } from './middleware/authMiddleware.js';
//import verificarApiKey from './middleware/apiKeyMiddleware.js';
import testRoute from './routes/testRoute.js';
import authRoute from './routes/authRoute.js';
import swaggerUi from 'swagger-ui-express';
import swaggerJSDoc from 'swagger-jsdoc';
import facturacionRoute from './routes/facturacionRoute.js';
import constantesRoute from './routes/constantesRoute.js';
import logger from './logger.js';
const app = express();
const port = process.env.PORT || 3000;
//// Middleware de logging simple
//app.use((req, res, next) => {
//    console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
//    next();
//});
app.use((req, res, next) => {
    logger.info(`${req.method} ${req.url} IP:${req.ip}`);
    next();
});
// Protecci�n con Helmet
app.use(helmet());
// Limitador de peticiones
const limiter = rateLimit({
    windowMs: 60 * 1000, // 1 minuto
    max: 100,
    message: "�Uy, demasiadas peticiones! ",
    standardHeaders: true,
    handler: (req, res) => {
        console.log(` IP ${req.ip} BLOQUEADA por rate limit!`);
        res.status(429).json({ error: "�Demasiadas peticiones!" });
    },
});
app.use(limiter);
const swaggerDefinition = {
    openapi: '3.0.0',
    info: {
        title: 'API Facturacion',
        version: '1.0.0',
        description: 'Documentacion de la API de Facturacion',
    },
    servers: [
        {
            url: 'http://localhost:3000',
        },
    ],
    components: {
        securitySchemes: {
            ApiKeyAuth: {
                type: 'apiKey',
                in: 'header',
                name: 'x-api-key',
                description: 'API Key para autenticacion',
            },
            BearerAuth: {
                type: 'http',
                scheme: 'bearer',
                bearerFormat: 'JWT',
                description: 'JWT para autenticacion',
            },
        },
    },
    // Puedes definir seguridad global o por endpoint
    // security: [
    //     { ApiKeyAuth: [] },
    //     { BearerAuth: [] }
    // ],
};
const options = {
    swaggerDefinition,
    apis: ['./routes/*.ts'], // Aseg�rate que la ruta incluya facturacionRoute.ts
};
const swaggerSpec = swaggerJSDoc(options);
app.use(express.json());
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));
// Rutas p�blicas (no requieren token)
authRoute(app);
constantesRoute(app);
// Middleware para proteger todas las rutas siguientes (JWT)
app.use(verificarToken);
// Rutas privadas (requieren token)
testRoute(app);
facturacionRoute(app);
// Middleware para manejar errores
app.use((err, req, res, next) => {
    logger.error(`Error en ${req.method} ${req.url}: ${err.message}`);
    res.status(500).json({ error: 'Error interno del servidor' });
});
app.listen(port, () => {
    console.log(`API escuchando en http://localhost:${port}`);
});
//# sourceMappingURL=app.js.map