using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using layerEntidad;
using layerDatos;

namespace layerNegocio
{
    public  class CN_Categorias
    {
        private CD_Categorias objCapaDatos = new CD_Categorias();

        public List<Categorias> ListarCategorias()
        {
            return objCapaDatos.ListarCategorias();
        }

        public int AgregarCategoria(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionCategoria) || string.IsNullOrWhiteSpace(obj.DescripcionCategoria))
            {
                Mensaje = "El nombre de categoria no puede quedar vacío!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.AgregarCategoria(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool ModificarCategoria(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.DescripcionCategoria) || string.IsNullOrWhiteSpace(obj.DescripcionCategoria))
            {
                Mensaje = "El nombre de categoria no puede quedar vacío!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDatos.ModificarCategoria(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool EliminarCategoria(int IdCate, out string Mensaje)
        {

            return objCapaDatos.EliminarCategoria(IdCate, out Mensaje);

        }
    }
}
