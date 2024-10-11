using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Reestablecer { get; set; }
        public bool Estado { get; set; }
        public string Fecha { get; set; }
        public string Error { get; set; }

    }
}
