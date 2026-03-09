export class HttpError extends Error {
    statusCode: number;
    details: Record<string, unknown> | undefined;

    constructor(statusCode: number, message: string, details?: Record<string, unknown>) {
        super(message);
        this.name = 'HttpError';
        this.statusCode = statusCode;
        this.details = details;
    }
}

export function toErrorMessage(error: unknown): string {
    return error instanceof Error ? error.message : String(error);
}
