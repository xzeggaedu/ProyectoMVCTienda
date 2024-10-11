using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerEntidad
{
    public class Distritos
    {
        public int IdDistrito {  get; set; }
        public string NombreDistrito { get; set; }
        public Departamentos oDepartamento { get; set; }

    }
}
