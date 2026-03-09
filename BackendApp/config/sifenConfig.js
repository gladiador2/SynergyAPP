import { HttpError } from '../utils/errors.js';
function getRequiredEnv(name, errorMessage) {
    const value = process.env[name];
    if (!value || value.trim() === '') {
        throw new HttpError(500, errorMessage);
    }
    return value;
}
function getAmbienteFromEnv() {
    const ambiente = getRequiredEnv('Ambiente', 'Variable de entorno Ambiente incompleta');
    if (ambiente !== 'test' && ambiente !== 'prod') {
        throw new HttpError(500, 'Ambiente invalido');
    }
    return ambiente;
}
export function loadSifenConfig() {
    return {
        certData: getRequiredEnv('Certificado_p12', 'Variables de entorno de certificado incompletas'),
        clave: getRequiredEnv('Clave', 'Variables de entorno de certificado incompletas'),
        idCSC: getRequiredEnv('idCSC', 'Variables de entorno de QR incompletas'),
        csc: getRequiredEnv('CSC', 'Variables de entorno de QR incompletas'),
        ambiente: getAmbienteFromEnv(),
    };
}
export function loadKudeConfig() {
    const logo = getRequiredEnv('logo', 'Variables de entorno de KUDE incompletas').trim();
    if (/\s/.test(logo)) {
        throw new HttpError(400, "La ruta 'logo' contiene espacios. Usa una ruta sin espacios o una URL sin espacios.");
    }
    const srcJasperRaw = getRequiredEnv('srcJasper', 'Variables de entorno de KUDE incompletas');
    return {
        logo,
        java8Path: getRequiredEnv('java8path', 'Variables de entorno de KUDE incompletas'),
        srcJasper: /[\\\/]$/.test(srcJasperRaw) ? srcJasperRaw : `${srcJasperRaw}\\`,
        destFolder: getRequiredEnv('destFolder', 'Variables de entorno de KUDE incompletas'),
    };
}
//# sourceMappingURL=sifenConfig.js.map