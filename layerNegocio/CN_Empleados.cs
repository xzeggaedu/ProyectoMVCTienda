using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using layerDatos;
using layerEntidad;

namespace layerNegocio
{
    public class CN_Empleados
    {
        private CD_Empleados _empleadoRepository;

        public CN_Empleados()
        {
            _empleadoRepository = new CD_Empleados();
        }

        public ResultadoOperacion<List<Empleados>> ListarEmpleados()
        {
            ResultadoOperacion<List<Empleados>> resultado = new ResultadoOperacion<List<Empleados>>();

            try
            {
                var empleados = _empleadoRepository.ListarEmpleados(); 
                resultado.Exito = true;
                resultado.Mensaje = "Empleados listados correctamente.";
                resultado.Datos = empleados; 
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error al listar los empleados: " + ex.Message;
                resultado.Datos = null;
            }

            return resultado;
        }

        private ResultadoOperacion<Empleados> ValidarEmpleado(Empleados oEmpleado)
        {
            ResultadoOperacion<Empleados> resultado = new ResultadoOperacion<Empleados>();

            // Validación de los Nombres
            if (string.IsNullOrEmpty(oEmpleado.Nombres))
            {
                resultado.Mensaje = "El nombre del empleado es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            if (oEmpleado.Nombres.Length > 100)
            {
                resultado.Mensaje = "El nombre del empleado no puede exceder los 100 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de los Apellidos
            if (string.IsNullOrEmpty(oEmpleado.Apellidos))
            {
                resultado.Mensaje = "El apellido del empleado es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            if (oEmpleado.Apellidos.Length > 100)
            {
                resultado.Mensaje = "El apellido del empleado no puede exceder los 100 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Dui
            if (string.IsNullOrEmpty(oEmpleado.Dui))
            {
                resultado.Mensaje = "El DUI del empleado es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación de la Dirección
            if (string.IsNullOrEmpty(oEmpleado.Direccion))
            {
                resultado.Mensaje = "La dirección del empleado es obligatoria.";
                resultado.Exito = false;
                return resultado;
            }

            if (oEmpleado.Direccion.Length > 255)
            {
                resultado.Mensaje = "La dirección del empleado no puede exceder los 255 caracteres.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Email
            if (string.IsNullOrEmpty(oEmpleado.Email))
            {
                resultado.Mensaje = "El email del empleado es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            // Validación del Teléfono
            if (string.IsNullOrEmpty(oEmpleado.Telefono))
            {
                resultado.Mensaje = "El teléfono del empleado es obligatorio.";
                resultado.Exito = false;
                return resultado;
            }

            resultado.Exito = true;
            return resultado;
        }

        public ResultadoOperacion<Empleados> AgregarEmpleado(Empleados oEmpleado)
        {
            // Validación previa del empleado
            ResultadoOperacion<Empleados> resultadoValidacion = ValidarEmpleado(oEmpleado);

            if (!resultadoValidacion.Exito)
            {
                return resultadoValidacion;
            }

            ResultadoOperacion<Empleados> resultado = new ResultadoOperacion<Empleados>();

            try
            {
                // Llamada al repositorio para agregar el empleado
                Empleados savedEmpleado = _empleadoRepository.AgregarEmpleado(oEmpleado);

                if (savedEmpleado == null)
                {
                    resultado.Mensaje = "Error al agregar el empleado. Intente nuevamente.";
                    resultado.Exito = false;
                    resultado.Datos = null;
                }
                else
                {
                    resultado.Mensaje = "Empleado agregado exitosamente.";
                    resultado.Exito = true;
                    resultado.Datos = savedEmpleado;
                }
            }
            catch (SqlException sqlEx)
            {
                resultado.Mensaje = "Error de base de datos al agregar el empleado: " + sqlEx.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = "Error inesperado al agregar el empleado: " + ex.Message;
                resultado.Exito = false;
                resultado.Datos = null;
            }

            return resultado;
        }
    }
}
