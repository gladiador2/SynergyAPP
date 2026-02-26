import type { Response } from 'express';

export function handleError(
    res: Response,
    status: number,
    error: unknown,
    extra?: Record<string, unknown>
) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    res.status(status).json({ success: false, error: errorMessage, ...extra });
}

export function validateNumberParam(val: unknown): val is number {
    return typeof val === 'number' && !isNaN(val);
}

export function sendConstantes(res: Response, data: unknown) {
    res.json({ success: true, data });
}
