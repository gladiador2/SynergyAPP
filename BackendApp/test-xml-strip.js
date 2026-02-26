const xml1 = '<?xml version="1.0" encoding="UTF-8"?>\n<DE>contenido</DE>';
const xml2 = '<?xml version="1.0" encoding="UTF-8"?>' + '<DE>contenido</DE>';
function strip(s) {
  return s.replace(/^\uFEFF/, '').replace(/^\s*<\?xml[^>]*\?>\s*/i, '');
}
console.log('1:', strip(xml1));
console.log('2:', strip(xml2));
