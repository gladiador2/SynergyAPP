import { Router } from 'express';
import qr from 'facturacionelectronicapy-qrgen';
import { handleError } from './utils.js';
const qrgen = qr.default;
const router = Router();
router.post('/generateQR', async (req, res) => {
    const { xmlSigned, idCSC, CSC, env } = req.body;
    try {
        const result = await qrgen.generateQR(xmlSigned, idCSC, CSC, env);
        res.json({ success: true, data: result });
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
export default router;
//# sourceMappingURL=qrgenRoutes.js.map