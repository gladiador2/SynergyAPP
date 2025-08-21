import type { Router, Request, Response } from 'express';
import jwt from 'jsonwebtoken';
import bcrypt from 'bcrypt';
import dotenv from 'dotenv';
import pool from '../db.js';
import { body, validationResult } from 'express-validator';
import logger from '../logger.js';

dotenv.config();

const MAX_INTENTOS = Number(process.env.MAX_INTENTOS) || 5;
const VENTANA_MINUTOS = Number(process.env.VENTANA_MINUTOS) || 1;


async function obtenerUsuario(username: string) {
    const result = await pool.query(
        'SELECT id_usuario, nombre_usuario, nombre_completo, email, hash_password FROM usuarios WHERE nombre_usuario = $1 AND estado = $2',
        [username, 'activo']
    );
    return result.rows[0];
}

async function obtenerIntentosFallidos(id_usuario: number) {
    const result = await pool.query(
        `SELECT fecha_acceso FROM accesos_sistema
         WHERE id_usuario = $1 AND exito = FALSE AND fecha_acceso > NOW() - INTERVAL '${VENTANA_MINUTOS} minutes'
         ORDER BY fecha_acceso ASC`,
        [id_usuario]
    );
    return result.rows;
}

function calcularMinutosRestantes(primerIntento: Date) {
    const ahora = new Date();
    const msPasados = ahora.getTime() - primerIntento.getTime();
    const minutosPasados = Math.floor(msPasados / 60000);
    return Math.max(VENTANA_MINUTOS - minutosPasados, 1);
}

async function registrarAcceso(id_usuario: number, ip: string, exito: boolean, motivo_fallo: string | null) {
    await pool.query(
        `INSERT INTO accesos_sistema (id_usuario, ip_origen, exito, motivo_fallo)
         VALUES ($1, $2, $3, $4)`,
        [id_usuario, ip, exito, motivo_fallo]
    );
}

async function actualizarUltimoLogin(id_usuario: number) {
    await pool.query(
        `UPDATE usuarios SET ultimo_login = NOW() WHERE id_usuario = $1`,
        [id_usuario]
    );
}

async function obtenerRoles(id_usuario: number) {
    const result = await pool.query(
        `SELECT r.id_rol, r.nombre_rol
         FROM usuarios_roles ur
         INNER JOIN roles r ON ur.id_rol = r.id_rol
         WHERE ur.id_usuario = $1`,
        [id_usuario]
    );
    return result.rows;
}

async function obtenerPermisos(ids_roles: number[]) {
    if (ids_roles.length === 0) return [];
    const result = await pool.query(
        `SELECT DISTINCT p.id_permiso, p.nombre_permiso
         FROM roles_permisos rp
         INNER JOIN permisos p ON rp.id_permiso = p.id_permiso
         WHERE rp.id_rol = ANY($1::int[])`,
        [ids_roles]
    );
    return result.rows;
}

function generarToken(user: any, roles: any[], permisos: any[]) {
    return jwt.sign(
        {
            id: user.id_usuario,
            username: user.nombre_usuario,
            roles: roles.map(r => r.nombre_rol),
            permisos: permisos.map(p => p.nombre_permiso)
        },
        process.env.JWT_SECRET || 'jwt_secreto_ultra_seguro',
        { expiresIn: '1h' }
    );
}

export default function (router: Router) {
    /**
 * @openapi
 * /api/login:
 *   post:
 *     summary: Autentica a un usuario y devuelve un token JWT.
 *     tags:
 *       - Autenticación
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - username
 *               - password
 *             properties:
 *               username:
 *                 type: string
 *                 example: usuario123
 *               password:
 *                 type: string
 *                 example: MiContraseñaSegura
 *     responses:
 *       200:
 *         description: Autenticación exitosa.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 message:
 *                   type: string
 *                 data:
 *                   type: object
 *                   properties:
 *                     token:
 *                       type: string
 *                     usuario:
 *                       type: object
 *                       properties:
 *                         id:
 *                           type: integer
 *                         username:
 *                           type: string
 *                         nombre:
 *                           type: string
 *                         email:
 *                           type: string
 *                     roles:
 *                       type: array
 *                       items:
 *                         type: object
 *                         properties:
 *                           id_rol:
 *                             type: integer
 *                           nombre_rol:
 *                             type: string
 *                     permisos:
 *                       type: array
 *                       items:
 *                         type: object
 *                         properties:
 *                           id_permiso:
 *                             type: integer
 *                           nombre_permiso:
 *                             type: string
 *                 errorCode:
 *                   type: string
 *                   nullable: true
 *                 sessionEnded:
 *                   type: boolean
 *       400:
 *         description: Datos inválidos o faltantes.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 message:
 *                   type: string
 *                 data:
 *                   type: array
 *                   items:
 *                     type: object
 *                 errorCode:
 *                   type: string
 *                 sessionEnded:
 *                   type: boolean
 *       401:
 *         description: Credenciales inválidas o contraseña incorrecta.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 message:
 *                   type: string
 *                 data:
 *                   type: object
 *                   nullable: true
 *                 errorCode:
 *                   type: string
 *                 sessionEnded:
 *                   type: boolean
 *       429:
 *         description: Demasiados intentos fallidos.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 message:
 *                   type: string
 *                 data:
 *                   type: object
 *                   properties:
 *                     minutos_restantes:
 *                       type: integer
 *                 errorCode:
 *                   type: string
 *                 sessionEnded:
 *                   type: boolean
 *       500:
 *         description: Error interno del servidor.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 success:
 *                   type: boolean
 *                 message:
 *                   type: string
 *                 data:
 *                   type: object
 *                   nullable: true
 *                 errorCode:
 *                   type: string
 *                 sessionEnded:
 *                   type: boolean
 */
    router.post('/api/login',
        [
            body('username').isString().notEmpty().withMessage('El usuario es requerido'),
            body('password').isString().notEmpty().withMessage('La contraseña es requerida')
        ],
        async (req: Request, res: Response) => {
            const errors = validationResult(req);
            if (!errors.isEmpty()) {
                return res.status(400).json({
                    success: false,
                    message: 'Datos inválidos',
                    data: errors.array(),
                    errorCode: 'VALIDATION_ERROR',
                    sessionEnded: false
                });
            }
        const { username, password } = req.body;
        const ip = req.ip ?? 'desconocida';

        if (!username || !password) {
            return res.status(400).json({
                success: false,
                message: 'Usuario y contraseña son requeridos',
                data: null,
                errorCode: 'AUTH_MISSING_FIELDS',
                sessionEnded: false
            });
        }

        try {
            const user = await obtenerUsuario(username);
            if (!user) {
                return res.status(401).json({
                    success: false,
                    message: 'Credenciales inválidas',
                    data: null,
                    errorCode: 'AUTH_INVALID_CREDENTIALS',
                    sessionEnded: false
                });
            }

            const intentos = await obtenerIntentosFallidos(user.id_usuario);
            if (intentos.length >= MAX_INTENTOS) {
                const minutosRestantes = calcularMinutosRestantes(new Date(intentos[0].fecha_acceso));
                return res.status(429).json({
                    success: false,
                    message: 'Demasiados intentos fallidos. Intenta más tarde.',
                    data: { minutos_restantes: minutosRestantes },
                    errorCode: 'AUTH_TOO_MANY_ATTEMPTS',
                    sessionEnded: false
                });
            }

            const match = await bcrypt.compare(password, user.hash_password);
            if (!match) {
                await registrarAcceso(user.id_usuario, ip, false, 'Contraseña incorrecta');
                return res.status(401).json({
                    success: false,
                    message: 'Contraseña incorrecta',
                    data: null,
                    errorCode: 'AUTH_WRONG_PASSWORD',
                    sessionEnded: false
                });
            }

            await registrarAcceso(user.id_usuario, ip, true, null);
            await actualizarUltimoLogin(user.id_usuario);

            const roles = await obtenerRoles(user.id_usuario);
            const permisos = await obtenerPermisos(roles.map(r => r.id_rol));
            const token = generarToken(user, roles, permisos);

            res.json({
                success: true,
                message: 'Autenticación exitosa',
                data: {
                    token,
                    usuario: {
                        id: user.id_usuario,
                        username: user.nombre_usuario,
                        nombre: user.nombre_completo,
                        email: user.email
                    },
                    roles,
                    permisos
                },
                errorCode: null,
                sessionEnded: false
            });

        } catch (err) {
            logger.error('Error en login:', err);
            
            res.status(500).json({
                success: false,
                message: 'Error interno del servidor',
                data: null,
                errorCode: 'SERVER_ERROR',
                sessionEnded: false
            });
        }
    });


    router.post('/api/crear-hash', async (req: Request, res: Response) => {
        const { password } = req.body;

        if (!password) {
            return res.status(400).json({ error: 'Debes enviar password ' });
        }

        try {
            const hash = await bcrypt.hash(password, 10);
            res.json(hash);
        } catch (err) {
            res.status(500).json({ error: 'Error al comparar el hash', detalle: err });
        }
    });
}
