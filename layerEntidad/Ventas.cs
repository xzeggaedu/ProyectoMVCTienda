using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class Ventas
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public float TotalProductos { get; set; }
        public decimal MontoTotal { get; set; }
        public string Contacto { get; set; }
        public Distritos oDistrito { get; set; }
        public string Telefono { get; set; }
        public string Direccion {  get; set; }
        public string FechaVenta { get; set; }
        public int IdTransaccion { get; set; }

    }
}
