import type { Request, Response, NextFunction } from 'express';
interface Cliente {
    id: number;
    nombre: string;
}
interface RequestConCliente extends Request {
    cliente?: Cliente;
}
export default function apiKeyMiddleware(req: RequestConCliente, res: Response, next: NextFunction): Response<any, Record<string, any>> | undefined;
export {};
//# sourceMappingURL=apiKeyMiddleware.d.ts.map