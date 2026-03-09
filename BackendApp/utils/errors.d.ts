export declare class HttpError extends Error {
    statusCode: number;
    details: Record<string, unknown> | undefined;
    constructor(statusCode: number, message: string, details?: Record<string, unknown>);
}
export declare function toErrorMessage(error: unknown): string;
//# sourceMappingURL=errors.d.ts.map