using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using layerDatos;
using layerEntidad;

namespace layerNegocio
{
    public class CN_Productos
    {
        private CD_Productos _productoRepository;

        public CN_Productos()
        {
            _productoRepository = new CD_Productos();
        }


        public ResultadoOperacion<List<Productos>> ListarProductos()
        {
            ResultadoOperacion<List<Productos>> resultado = new ResultadoOperacion<List<Productos>>();

            try
            {
                var productos = _productoRepository.ListarProductos(); // Llama al repositorio para listar los productos
                resultado.Exito = true;
                resultado.Mensaje = "Productos listados correctamente.";
                resultado.Datos = productos; // Almacena la lista de productos en el campo Datos
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error al listar los productos: " + ex.Message;
                resultado.Datos = null;
            }

            return resultado;
        }


        private ResultadoOperacion<Productos> ValidarProducto(Productos oProducto)
        {
            ResultadoOperacion<Productos> resultado = new ResultadoOperacion<Productos>();

            // Validación del Nombre del Producto
            if (string.IsNullOrEmpty(oProducto.NombreProducto))
            {
                resultado.Mensaje = "El nombre del producto es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            if (oProducto.NombreProducto.Length > 100)
            {
                resultado.Mensaje = "El nombre del producto no puede exceder los 100 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de la Descripción
            if (string.IsNullOrEmpty(oProducto.Descripcion))
            {
                resultado.Mensaje = "La descripción del producto es obligatoria.";
                resultado.Exito = false;
                return resultado;
            }

            if (oProducto.Descripcion.Length > 500)
            {
                resultado.Mensaje = "La descripción del producto no puede exceder los 500 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de la Marca
            if (oProducto.oMarca == null || oProducto.oMarca.IdMarca <= 0)
            {
                resultado.Mensaje = "La marca del producto es inválida.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de la Categoría
            if (oProducto.oCategoria == null || oProducto.oCategoria.IdCategoria <= 0)
            {
                resultado.Mensaje = "La categoría del producto es inválida.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Precio
            if (oProducto.Precio <= 0)
            {
                resultado.Mensaje = "El precio del producto debe ser mayor a cero.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Stock
            if (oProducto.Stock < 0)
            {
                resultado.Mensaje = "El stock del producto no puede ser negativo.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de la Ruta de la Imagen
            if (string.IsNullOrEmpty(oProducto.RutaImagen))
            {
                resultado.Mensaje = "La ruta de la imagen del producto es obligatoria.";
                resultado.Exito = false;
                return resultado;
            }

            if (oProducto.RutaImagen.Length > 200)
            {
                resultado.Mensaje = "La ruta de la imagen del producto no puede exceder los 200 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Nombre de la Imagen
            if (string.IsNullOrEmpty(oProducto.NombreImagen))
            {
                resultado.Mensaje = "El nombre de la imagen del producto es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            if (oProducto.NombreImagen.Length > 100)
            {
                resultado.Mensaje = "El nombre de la imagen del producto no puede exceder los 100 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            resultado.Exito = true;
            return resultado;
        }


        public ResultadoOperacion<Productos> ObtenerProductoPorId(int id)
        {
            ResultadoOperacion<Productos> resultado = new ResultadoOperacion<Productos>();

            // Validación del ID
            if (id <= 0)
            {
                resultado.Mensaje = "El ID del producto no es válido.";
                resultado.Exito = false;
                return resultado;
            }

            try
            {
                // Llamada al repositorio para obtener el producto
                Productos oProducto = _productoRepository.ObtenerProductoPorId(id);

                // Validación de si se encontró el producto
                if (oProducto != null && oProducto.IdProducto > 0)
                {
                    resultado.Exito = true;
                    resultado.Mensaje = "Producto obtenido correctamente.";
                    resultado.Datos = oProducto;
                }
                else
                {
                    resultado.Exito = false;
                    resultado.Mensaje = "No se encontró un producto con el ID proporcionado.";
                    resultado.Datos = null;
                }
            }
            catch (SqlException sqlEx)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error de base de datos al obtener el producto: " + sqlEx.Message;
                resultado.Datos = null;
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error inesperado al obtener el producto: " + ex.Message;
                resultado.Datos = null;
            }

            return resultado;
        }


        public ResultadoOperacion<Productos> AgregarProducto(Productos oProducto)
        {
            // Validación previa del producto
            ResultadoOperacion<Productos> resultadoValidacion = ValidarProducto(oProducto);

            if (!resultadoValidacion.Exito)
            {
                return resultadoValidacion;
            }

            ResultadoOperacion<Productos> resultado = new ResultadoOperacion<Productos>();

            try
            {
                // Llamada al repositorio para agregar el producto
                Productos savedProducto = _productoRepository.AgregarProducto(oProducto);

                if (savedProducto == null)
                {
                    resultado.Mensaje = "Error al agregar el producto. Intente nuevamente.";
                    resultado.Exito = false;
                    resultado.Datos = null;
                }
                else
                {
                    resultado.Mensaje = "Producto agregado exitosamente.";
                    resultado.Exito = true;
                    resultado.Datos = savedProducto;
                }
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al agregar el producto: " + sqlEx.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al agregar el producto: " + ex.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }

            return resultado;
        }


        public ResultadoOperacion<Productos> ActualizarProducto(Productos oProducto)
        {
            // Validación previa del producto
            ResultadoOperacion<Productos> resultadoValidacion = ValidarProducto(oProducto);

            if (!resultadoValidacion.Exito)
            {
                return resultadoValidacion;
            }

            ResultadoOperacion<Productos> resultado = new ResultadoOperacion<Productos>();

            try
            {
                // Llamada al repositorio para actualizar el producto
                _productoRepository.ActualizarProducto(oProducto);

                resultado.Exito = true;
                resultado.Mensaje = "Producto actualizado exitosamente.";
                resultado.Datos = oProducto;
            }
            catch (SqlException sqlEx)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error de base de datos al actualizar el producto: " + sqlEx.Message;
                resultado.Datos = null;
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error inesperado al actualizar el producto: " + ex.Message;
                resultado.Datos = null;
            }

            return resultado;
        }


        public ResultadoOperacion<object> EliminarProducto(int id)
        {
            ResultadoOperacion<object> resultado = new ResultadoOperacion<object>();

            // Validación del ID
            if (id <= 0)
            {
                resultado.Mensaje = "El ID del producto no es válido.";
                resultado.Exito = false;
                return resultado;
            }

            try
            {
                // Llamada al repositorio para eliminar el producto
                _productoRepository.EliminarProducto(id);

                resultado.Mensaje = "Producto eliminado exitosamente.";
                resultado.Exito = true;
                resultado.Datos = null;
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al eliminar el producto: " + sqlEx.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al eliminar el producto: " + ex.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }

            return resultado;
        }
    }
}
