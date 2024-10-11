using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using layerDatos;
using layerEntidad;

namespace layerNegocio
{
    public class CN_Marcas
    {
        private CD_Marcas _marcaRepository;

        public CN_Marcas()
        {
            _marcaRepository = new CD_Marcas();
        }

        public ResultadoOperacion<List<Marca>> ListarMarcas()
        {
            ResultadoOperacion<List<Marca>> resultado = new ResultadoOperacion<List<Marca>>();

            try
            {
                List<Marca> marcas = _marcaRepository.ListarMarcas();  // Llama al repositorio para listar las marcas
                resultado.Exito = true;
                resultado.Mensaje = "Marcas listadas correctamente";
                resultado.Datos = marcas;  // Almacena la lista de marcas en el campo Datos
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error al listar las marcas: " + ex.Message;
                resultado.Datos = null;
            }

            return resultado;
        }

        private ResultadoOperacion<Marca> ValidarMarca(Marca oMarca)
        {
            ResultadoOperacion<Marca> resultado = new ResultadoOperacion<Marca>();

            if (string.IsNullOrEmpty(oMarca.NombreMarca))
            {
                resultado.Mensaje = "El nombre de la marca es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            if (oMarca.NombreMarca.Length > 50)
            {
                resultado.Mensaje = "El nombre de la marca no puede exceder los 50 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            resultado.Exito = true;
            return resultado;
        }

        public ResultadoOperacion<Marca> ObtenerMarcaPorId(int id)
        {
            ResultadoOperacion<Marca> resultado = new ResultadoOperacion<Marca>();

            // Validación del ID
            if (id <= 0)
            {
                resultado.Mensaje = "El ID de la marca no es válido.";
                resultado.Exito = false;
                return resultado;
            }

            try
            {
                // Llamada al repositorio para obtener la marca
                Marca oMarca = _marcaRepository.ObtenerMarcaPorId(id);

                // Validación de si se encontró la marca
                if (oMarca != null && oMarca.IdMarca > 0)
                {
                    resultado.Exito = true;
                    resultado.Datos = oMarca;
                }
                else
                {
                    resultado.Exito = false;
                    resultado.Mensaje = "No se encontró una marca con el ID proporcionado.";
                }
            }
            catch (SqlException sqlEx)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error de base de datos al obtener la marca: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error inesperado al obtener la marca: " + ex.Message;
            }

            return resultado;
        }

        public ResultadoOperacion<Marca> AgregarMarca(Marca oMarca)
        {
            ResultadoOperacion<Marca> resultado = ValidarMarca(oMarca);

            if (!resultado.Exito)
            {
                return resultado;
            }

            try
            {
                Marca savedMarca = _marcaRepository.AgregarMarca(oMarca);

                if (savedMarca == null)
                {
                    resultado.Mensaje = "Error al agregar la marca. Intente nuevamente.";
                    resultado.Exito = false;
                }
                else
                {
                    resultado.Mensaje = "Marca agregada exitosamente.";
                    resultado.Exito = true;
                    resultado.Datos = savedMarca;
                }
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al agregar la marca: " + sqlEx.Message;
                resultado.Exito = false;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al agregar la marca: " + ex.Message;
                resultado.Exito = false;
            }

            return resultado;
        }

        public ResultadoOperacion<Marca> ActualizarMarca(Marca oMarca)
        {
            ResultadoOperacion<Marca> resultado = ValidarMarca(oMarca);

            // Si la validación falla, se devuelve el resultado con el mensaje correspondiente
            if (!resultado.Exito)
            {
                return resultado;
            }

            try
            {
                // Llamada al repositorio para actualizar la marca
                _marcaRepository.ActualizarMarca(oMarca);

                resultado.Mensaje = "Marca actualizada exitosamente.";
                resultado.Exito = true;
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al actualizar la marca: " + sqlEx.Message;
                resultado.Exito = false;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al actualizar la marca: " + ex.Message;
                resultado.Exito = false;
            }

            return resultado;
        }

        public ResultadoOperacion<object> EliminarMarca(int id)
        {
            ResultadoOperacion<object> resultado = new ResultadoOperacion<object>();

            // Validación del ID
            if (id <= 0)
            {
                resultado.Mensaje = "El ID de la marca no es válido.";
                resultado.Exito = false;
                return resultado;
            }

            try
            {
                // Llamada al repositorio para eliminar la marca
                _marcaRepository.EliminarMarca(id);

                resultado.Mensaje = "Marca eliminada exitosamente.";
                resultado.Exito = true;
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al eliminar la marca: " + sqlEx.Message;
                resultado.Exito = false;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al eliminar la marca: " + ex.Message;
                resultado.Exito = false;
            }

            return resultado;
        }

    }

}
