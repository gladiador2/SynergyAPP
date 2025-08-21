import type { Request, Response, NextFunction } from 'express';
import jwt from 'jsonwebtoken';
import dotenv from 'dotenv';

dotenv.config();

export interface AuthRequest extends Request {
    user?: any;
}

export function verificarToken(req: AuthRequest, res: Response, next: NextFunction) {
    const authHeader = req.headers['authorization'];
    if (!authHeader || !authHeader.startsWith('Bearer ')) {
        return res.status(401).json({
            success: false,
            message: 'Formato de token inválido',
            data: null,
            errorCode: 'AUTH_INVALID_FORMAT',
            sessionEnded: true
        });
    }

    const token = authHeader.split(' ')[1];
    if (!token) {
        return res.status(401).json({
            success: false,
            message: 'Token no proporcionado',
            data: null,
            errorCode: 'AUTH_NO_TOKEN',
            sessionEnded: true
        });
    }

    try {
        const decoded = jwt.verify(token, process.env.JWT_SECRET || 'jwt_secreto_ultra_seguro');
        req.user = decoded;
        next();
    } catch (err: any) {
        if (err.name === 'TokenExpiredError') {
            return res.status(401).json({
                success: false,
                message: 'Sesión expirada. Por favor, inicie sesión nuevamente.',
                data: null,
                errorCode: 'AUTH_SESSION_EXPIRED',
                sessionEnded: true
            });
        }
        return res.status(403).json({
            success: false,
            message: 'Token inválido',
            data: null,
            errorCode: 'AUTH_INVALID_TOKEN',
            sessionEnded: true
        });
    }
}
