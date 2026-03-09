export function normalizeXML(xml) {
    return xml
        .replace(/\r\n/g, '')
        .replace(/\n/g, '')
        .replace(/\t/g, '')
        .replace(/\s{2,}/g, ' ')
        .replace(/>\s+</g, '><')
        .trim();
}
//# sourceMappingURL=xmlUtils.js.map