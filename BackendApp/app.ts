import 'dotenv/config';
import express from 'express';
import cors from 'cors';
import helmet from 'helmet';
import rateLimit from 'express-rate-limit';
import { verificarToken } from './middleware/authMiddleware.js';
import testRoute from './routes/testRoute.js';
import authRoute from './routes/authRoute.js';
import swaggerUi from 'swagger-ui-express';
import swaggerJSDoc from 'swagger-jsdoc';
import path from 'path';
import { fileURLToPath } from 'url';
import facturacionRoute from './routes/facturacionRoute.js';
import constantesRoute from './routes/constantesRoute.js';
import apiRoute from './routes/apiRoute.js';
import logger from './logger.js';
import type { Request, Response, NextFunction } from 'express';


const app = express();
const port = process.env.PORT || 3000;
const swaggerServerUrl = process.env.SWAGGER_SERVER_URL || `http://localhost:${port}`;

// Opciones CORS más permisivas para desarrollo
const corsOptions: cors.CorsOptions = {
    origin: (origin, callback) => {
        // Permite cualquier origen en dev; ajusta a tu frontend en PROD
        callback(null, true);
    },
    credentials: true,
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization']
};

//// Middleware de logging simple
//app.use((req, res, next) => {
//    console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
//    next();
//});

app.use((req, res, next) => {
    logger.info(`${req.method} ${req.url} IP:${req.ip}`);
    next();
});


// CORS primero (antes de Helmet y rate limit) y preflight explícito
app.use(cors(corsOptions));
// Preflight: el middleware cors maneja OPTIONS automáticamente; no registrar ruta global

// Proteccin con Helmet
app.use(helmet());

// CORS: permite consumir la API desde otros orígenes (y evita errores en Swagger UI si el host cambia)
app.use(cors());

// Limitador de peticiones
const limiter = rateLimit({
    windowMs: 60 * 1000, // 1 minuto
    max: 100,
    message: "�Uy, demasiadas peticiones! ",
    standardHeaders: true,
    skip: (req) => req.method === 'OPTIONS',
    handler: (req, res) => {
        console.log(` IP ${req.ip} BLOQUEADA por rate limit!`);
        res.status(429).json({ error: "�Demasiadas peticiones!" });
    },
});
// Aplica rate limit después de CORS y Helmet, y omite OPTIONS
app.use(limiter);

const swaggerDefinition = {
    openapi: '3.0.0',
    info: {
        title: 'API Facturacion',
        version: '1.0.0',
        description: 'Documentacion de la API de Facturacion',
    },
    tags: [
        { name: 'Facturacion' },
        { name: 'xmlgen' },
        { name: 'setapi' },
        { name: 'xmlsign' },
        { name: 'kude' },
        { name: 'qrgen' }
    ],
    servers: [
        {
            // URL absoluta para evitar esquemas inválidos (file://, ws://) en Swagger UI
            url: swaggerServerUrl,
        },
    ],
    components: {
        securitySchemes: {
            BearerAuth: {
                type: 'http',
                scheme: 'bearer',
                bearerFormat: 'JWT',
                description: 'JWT para autenticacion',
            },
        },
    },
    // Puedes definir seguridad global o por endpoint
     security: [
         { BearerAuth: [] }
        
     ],
};
const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const options = {
    swaggerDefinition,
    apis: [
        path.join(__dirname, 'routes', '*.ts').replace(/\\/g, '/'),
        path.join(__dirname, 'routes', '*.js').replace(/\\/g, '/')
    ],
};

const swaggerSpec = swaggerJSDoc(options);
app.use(express.json({
    verify: (req, _res, buf) => {
        // Guarda el body original para recuperar numeros grandes sin redondeo de JavaScript.
        (req as Request & { rawBody?: string }).rawBody = buf.toString('utf8');
    },
}));
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));

// Rutas p�blicas (no requieren token)
authRoute(app);
constantesRoute(app);
app.use('/facturacion', facturacionRoute);
app.use('/api', apiRoute);
// Middleware para proteger todas las rutas siguientes (JWT)
app.use(verificarToken);

// Rutas privadas (requieren token)
testRoute(app);


// Middleware para manejar errores
app.use((err: Error, req: Request, res: Response, next: NextFunction) => {
    logger.error(`Error en ${req.method} ${req.url}: ${err.message}`);
    res.status(500).json({ error: 'Error interno del servidor' });
});

app.listen(port, () => {
    console.log(`API escuchando en http://localhost:${port}`);
});
