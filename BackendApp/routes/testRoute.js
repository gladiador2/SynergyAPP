import { requirePermiso } from '../middleware/authorizationMiddleware.js';
export default function (router) {
    /**
 * @openapi
 * /api/test:
 *   get:
 *     summary: Endpoint de prueba protegido por permiso
 *     tags:
 *       - Test
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Respuesta exitosa
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 status:
 *                   type: string
 *                 data:
 *                   type: object
 *                   properties:
 *                     mensaje:
 *                       type: string
 *                     valor:
 *                       type: integer
 *       403:
 *         description: Permiso denegado
 */
    router.get('/api/test', requirePermiso('ver_reporte'), (req, res) => {
        const isSuccess = true;
        const response = isSuccess
            ? { status: 'ok', data: { mensaje: '�Todo ok!', valor: 42 } }
            : { status: 'error', error: 'Algo sali� mal' };
        res.json(response);
    });
}
//# sourceMappingURL=testRoute.js.map