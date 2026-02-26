// Script de prueba para generar la URL de QR desde un XML firmado
// Uso: node scripts/test-qr.js <rutaXML> <IdCSC> <CSC> <env>
// Ejemplo: node scripts/test-qr.js Documentacion/xmlGenerado.xml 123456 "miCSCsecreto" prod

import fs from 'fs';
import path from 'path';
import qrgenModule from 'facturacionelectronicapy-qrgen/dist/QRGen.js';

const QRGen = qrgenModule.default || qrgenModule;

async function main() {
  const [xmlPathArg, idCSC, CSC, env] = process.argv.slice(2);
  const xmlPath = xmlPathArg || path.join('Documentacion', 'xmlGenerado.xml');

  if (!idCSC || !CSC) {
    console.error('Faltan argumentos: IdCSC y CSC son requeridos.');
    console.error('Uso: node scripts/test-qr.js <rutaXML> <IdCSC> <CSC> <env>');
    process.exit(1);
  }

  let xml;
  try {
    xml = fs.readFileSync(xmlPath, { encoding: 'utf8' });
  } catch (err) {
    console.error('No se pudo leer el archivo XML:', xmlPath);
    console.error(err?.message || err);
    process.exit(1);
  }

  try {
    const xmlWithQR = await QRGen.generateQR(xml, idCSC, CSC, env || 'prod');
    // Extraer la URL del QR desde el XML resultante
    const match = xmlWithQR.match(/<dCarQR[^>]*>([^<]+)<\/dCarQR>/);
    if (match && match[1]) {
      console.log('URL de QR generada:');
      console.log(match[1]);
    } else {
      console.log('No se encontró dCarQR. XML resultante completo:');
      console.log(xmlWithQR);
    }
  } catch (err) {
    console.error('Error generando el QR:', err?.message || err);
    process.exit(1);
  }
}

main();
