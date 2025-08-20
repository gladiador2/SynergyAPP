using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Models
{
    public class ModeloUsuario
    {
        public float id { get; set; }
        public Sucursal sucursal { get; set; }
        public string nombre { get; set; }
        public string[] roles { get; set; }
        public DateTime fecha_caducidad_pass { get; set; }
        public DateTime fecha_ultimo_login { get; set; }
        public string email { get; set; }
        public string usuario { get; set; }
        public string apellido { get; set; }
        public string token { get; set; }

        public bool RegistroVisitantes { get; set; }


    }



    public class Sucursal
    {
        public string nombre { get; set; }
        public string abreviatura { get; set; }
        public float id { get; set; }
    }


    public class UsuarioModel
    {
        public string token { get; set; }
        public Usuario usuario { get; set; }
        public Role[] roles { get; set; }
        public Permiso[] permisos { get; set; }
    }

    public class Usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
    }

    public class Role
    {
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
    }

    public class Permiso
    {
        public int id_permiso { get; set; }
        public string nombre_permiso { get; set; }
    }


}
