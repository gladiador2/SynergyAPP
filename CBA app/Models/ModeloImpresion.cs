using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Models
{
   public class ModeloImpresion
    {
        public class ObjetoImpresion
        {
            public Variablesimpresion variablesImpresion { get; set; }
            public Lineaimpresion[] lineaImpresion { get; set; }
            public Qr qr { get; set; }
        }

        public class Variablesimpresion
        {
            public int pageWidth { get; set; }
            public int pageHeight { get; set; }
            public string lineaConGuiones { get; set; }
            public int posXInicial { get; set; }
            public object QR { get; set; }
            public string cdc { get; set; }
            public string link { get; set; }
        }

        public class Qr
        {
            public string link { get; set; }
            public int posX { get; set; }
            public int posY { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Lineaimpresion
        {
            public string Campo { get; set; }
            public string fuente { get; set; }
            public float fuenteTamaño { get; set; }
            public string color { get; set; }
            public int posX { get; set; }
            public int posY { get; set; }
            public bool Negrita { get; set; }
            public bool esMultilinea { get; set; }

            public bool AlinearDerecha { get; set; }
            public bool AlinearIzquierda { get; set; }

            public bool Centrado { get; set; }
        }

    }
    public class FuenteBase
    {
        public virtual string Fuente { get { return "Arial"; } }
        public virtual float FuenteTamaño { get { return 7; } }
        public virtual bool Negrita { get { return false; } }
    }

    public class FuenteNegrita : FuenteBase
    {
        public override float FuenteTamaño { get { return 8; } }
        public override bool Negrita { get { return true; } }
    }

    public class FuenteConEspaciado : FuenteBase
    {
        public override string Fuente { get { return "Courier New"; } }
        public override float FuenteTamaño { get { return 10; } }
    }

}

