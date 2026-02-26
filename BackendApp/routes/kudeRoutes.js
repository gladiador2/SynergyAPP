import { Router } from 'express';
import kude from 'facturacionelectronicapy-kude';
import { handleError } from './utils.js';
const Kude = kude.default;
const router = Router();
router.post('/generateKUDE', async (req, res) => {
    const { java8Path, xmlSigned, urlLogo, ambiente } = req.body;
    try {
        const result = await Kude.generateKUDE(java8Path, xmlSigned, urlLogo, ambiente);
        res.json({ success: true, data: result });
    }
    catch (error) {
        handleError(res, 500, error);
    }
});
export default router;
//# sourceMappingURL=kudeRoutes.js.map