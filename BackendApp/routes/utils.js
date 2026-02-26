export function handleError(res, status, error, extra) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    res.status(status).json({ success: false, error: errorMessage, ...extra });
}
export function validateNumberParam(val) {
    return typeof val === 'number' && !isNaN(val);
}
export function sendConstantes(res, data) {
    res.json({ success: true, data });
}
//# sourceMappingURL=utils.js.map