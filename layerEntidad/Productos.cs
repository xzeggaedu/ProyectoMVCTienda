using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{

    public class Productos
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categorias oCategoria { get; set; }
        public decimal Precio { get; set; }
        public float Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
