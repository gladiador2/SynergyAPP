import type { NextFunction, Response } from "express";
import type { AuthRequest } from "./authMiddleware.js";
export declare function requirePermiso(nombrePermiso: string): (req: AuthRequest, res: Response, next: NextFunction) => Response<any, Record<string, any>> | undefined;
//# sourceMappingURL=authorizationMiddleware.d.ts.map