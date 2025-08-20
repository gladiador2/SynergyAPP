import jwt from 'jsonwebtoken';
import dotenv from 'dotenv';
dotenv.config();
export function verificarToken(req, res, next) {
    const authHeader = req.headers['authorization'];
    if (!authHeader || !authHeader.startsWith('Bearer ')) {
        return res.status(401).json({
            success: false,
            message: 'Formato de token inv�lido',
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
    }
    catch (err) {
        if (err.name === 'TokenExpiredError') {
            return res.status(401).json({
                success: false,
                message: 'Sesi�n expirada. Por favor, inicie sesi�n nuevamente.',
                data: null,
                errorCode: 'AUTH_SESSION_EXPIRED',
                sessionEnded: true
            });
        }
        return res.status(403).json({
            success: false,
            message: 'Token inv�lido',
            data: null,
            errorCode: 'AUTH_INVALID_TOKEN',
            sessionEnded: true
        });
    }
}
//# sourceMappingURL=authMiddleware.js.map