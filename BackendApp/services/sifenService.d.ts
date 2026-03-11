export interface EnviarDEInput {
    body: {
        params?: Record<string, unknown>;
        data?: Record<string, unknown>;
        config?: Record<string, unknown>;
    };
    usuarioId: number;
}
export interface EnviarDEResult {
    jsonId: number;
    xmlGenerado: string;
    xmlFirmado: string;
    xmlConQr: string;
    respuestaSifen: unknown;
    kude: unknown;
}
export interface EnviarDEInicialResult {
    jsonId: number;
    xmlGeneradoId: number;
    xmlGenerado: string;
    xmlFirmado: string;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}
export interface SifenRetryOptions {
    maxRetries: number;
    baseDelayMs: number;
    maxDelayMs: number;
}
export interface ProcesamientoAsincronoResult {
    respuestaSifen: unknown;
    kude: unknown;
}
export declare function recibirJsonYGenerarXmlInicial(input: EnviarDEInput): Promise<EnviarDEInicialResult>;
export declare function procesarFlujoAsincronoSifen(input: {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}): Promise<ProcesamientoAsincronoResult>;
export declare function procesarFlujoAsincronoSifenLote(input: {
    jsonId: number;
    xmlGeneradoId: number;
    xmlConQr: string;
    config: Record<string, unknown> | undefined;
}): Promise<ProcesamientoAsincronoResult>;
export declare function enviarDE(input: EnviarDEInput): Promise<EnviarDEResult>;
export declare function obtenerEstadoProceso(jsonId: number): Promise<{
    id: number;
    estado: string;
    error: string | null;
    fechaCreacion: Date;
}>;
export declare function consultarLoteSifen(input: {
    numeroLote: string;
    id?: number;
    config?: Record<string, unknown>;
}): Promise<unknown>;
//# sourceMappingURL=sifenService.d.ts.map