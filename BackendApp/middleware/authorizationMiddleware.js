export function requirePermiso(nombrePermiso) {
    return (req, res, next) => {
        const permisos = req.user?.permisos || [];
        if (!permisos.some((p) => p.nombre_permiso === nombrePermiso)) {
            return res.status(403).json({
                success: false,
                message: 'No tienes permiso para realizar esta acci�n',
                data: null,
                errorCode: 'PERMISSION_DENIED',
                sessionEnded: false
            });
        }
        next();
    };
}
//# sourceMappingURL=authorizationMiddleware.js.map