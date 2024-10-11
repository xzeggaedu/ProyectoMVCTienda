using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public Clientes oCliente {  get; set; }
        public Productos oProducto { get; set; }
        public float Cantidad { get; set; }
        public string FechaCarrito { get; set; }
    }
}
