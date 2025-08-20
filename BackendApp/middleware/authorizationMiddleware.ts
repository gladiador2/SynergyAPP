import type { NextFunction, Response } from "express";
import type { AuthRequest } from "./authMiddleware.js";
interface Permiso {
    id_permiso: number;
    nombre_permiso: string;
}
export function requirePermiso(nombrePermiso: string) {
    return (req: AuthRequest, res: Response, next: NextFunction) => {
        const permisos: Permiso[] = req.user?.permisos || [];
        if (!permisos.some((p: Permiso) => p.nombre_permiso === nombrePermiso)) {
            return res.status(403).json({
                success: false,
                message: 'No tienes permiso para realizar esta acción',
                data: null,
                errorCode: 'PERMISSION_DENIED',
                sessionEnded: false
            });
        }
        next();
    };
}

