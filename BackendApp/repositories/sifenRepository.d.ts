export interface SignedXmlRecordInput {
    xmlGeneradoId: number | null;
    datosXmlFirmado: string | null;
    estado: string;
    error: string | null;
    respuesta: string | null;
}
export declare function insertJsonRecibido(datosJson: unknown, estado: string, usuarioId: number): Promise<number>;
export declare function insertJsonRecibidoError(datosJson: unknown, estado: string, error: string, usuarioId: number): Promise<void>;
export declare function updateJsonRecibidoError(id: number, estado: string, error: string): Promise<void>;
export declare function updateJsonRecibidoEstado(id: number, estado: string, error?: string | null): Promise<void>;
export interface JsonRecibidoStatus {
    id: number;
    estado: string;
    error: string | null;
    fechaCreacion: Date;
}
export declare function getJsonRecibidoStatusById(id: number): Promise<JsonRecibidoStatus | null>;
export declare function insertXmlGenerado(datosXml: string, jsonId: number): Promise<number>;
export declare function insertXmlFirmado(input: SignedXmlRecordInput): Promise<void>;
export declare function updateXmlFirmadoRespuestaByXmlGeneradoId(xmlGeneradoId: number, estado: string, respuesta: string | null, error: string | null): Promise<void>;
//# sourceMappingURL=sifenRepository.d.ts.map