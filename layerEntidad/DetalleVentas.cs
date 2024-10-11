using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class DetalleVentas
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public Productos oProducto {  get; set; }
        public float Cantidad { get; set; }
        public decimal TotalDetalle { get; set; }

    }
}
