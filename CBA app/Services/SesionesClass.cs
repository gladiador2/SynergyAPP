using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Services
{
    public abstract class SesionesClass
    {
        
        public static readonly string[] RolesApp =
        {
            //"CONTROL OPERACIONES APP",
            //"DASHBOARD APP",
            //"REGISTRO DE VISITANTES APP",
            "GIO APP"
        };

        #region Sesion Rest
        public static object SesionRest => new { usuario_id =1, token = App.UserDetails.token };
        #endregion

        #region Sesion Movile
        public static object SesionMovile => new { usuarioId = 1, token = App.UserDetails.token, dispositivoId = -1.0 };
        #endregion

        public static bool RolTiene(string[] listaRoles, string rol)
        {
            return listaRoles.Contains(rol);
        }

        public static bool RolTiene(string rol)
        {
            return true;// App.UserDetails.roles.Contains(rol);
        }

        public static bool TieneAlgunPermiso(string[] listaRoles)
        {
           
            return RolesApp.Any(rol => RolTiene(listaRoles, rol));
           
        }
    }
}
