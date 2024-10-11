using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Restablecer { get; set; }
        public string FechaRegistro { get; set; }

    }
}
