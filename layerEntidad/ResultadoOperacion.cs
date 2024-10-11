using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class ResultadoOperacion<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; } = default; // Generico y opcional
    }
}
