import 'dotenv/config';
import express from 'express';
import helmet from 'helmet';
import rateLimit from 'express-rate-limit';
import { verificarToken } from './middleware/authMiddleware.js';
import testRoute from './routes/testRoute.js';
import authRoute from './routes/authRoute.js';
import swaggerUi from 'swagger-ui-express';
import swaggerJSDoc from 'swagger-jsdoc';
import facturacionRoute from './routes/facturacionRoute.js';
import constantesRoute from './routes/constantesRoute.js';
const app = express();
const port = process.env.PORT || 3000;
// Middleware de logging simple
app.use((req, res, next) => {
    console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
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
        title: 'API Facturaci�n',
        version: '1.0.0',
        description: 'Documentaci�n de la API de Facturaci�n',
    },
    servers: [
        {
            url: 'http://localhost:3000',
        },
    ],
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
facturacionRoute(app);
constantesRoute(app);
// Middleware para proteger todas las rutas siguientes
app.use(verificarToken);
// Rutas privadas (requieren token)
testRoute(app);
app.listen(port, () => {
    console.log(`API escuchando en http://localhost:${port}`);
});
//# sourceMappingURL=app.js.map