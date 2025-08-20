using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Models.GIO
{
    
    public class Campos
    {
        public string propiedad { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public int orden { get; set; }
    }

    public class ModeloFichas
    {
        public int id { get; set; }
        public List<Campos> campos { get; set; }
        public string NOMBRE { get; set; }
    }

    public class FichaResultadoBuscar
    {
        public string NOMBRE { get; set; }

        public FichaResultadoBuscar(string NOMBRE)
        {
            this.NOMBRE = NOMBRE;
        }
    }
}
