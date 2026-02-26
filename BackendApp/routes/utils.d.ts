import type { Response } from 'express';
export declare function handleError(res: Response, status: number, error: unknown, extra?: Record<string, unknown>): void;
export declare function validateNumberParam(val: unknown): val is number;
export declare function sendConstantes(res: Response, data: unknown): void;
//# sourceMappingURL=utils.d.ts.map