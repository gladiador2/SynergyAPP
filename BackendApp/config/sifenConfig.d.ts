export type Ambiente = 'test' | 'prod';
export interface SifenConfig {
    certData: string;
    clave: string;
    idCSC: string;
    csc: string;
    ambiente: Ambiente;
}
export interface KudeConfig {
    logo: string;
    java8Path: string;
    srcJasper: string;
    destFolder: string;
}
export declare function loadSifenConfig(): SifenConfig;
export declare function loadKudeConfig(): KudeConfig;
//# sourceMappingURL=sifenConfig.d.ts.map