using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; 
using layerEntidad;

namespace layerDatos
{
    public class CD_Empleados
    {
        private SqlConnection connection;
        private string Patron; 

        public CD_Empleados()
        {
            connection = new SqlConnection(Conexion.cn);
            Patron = ConfigurationManager.AppSettings["Patron"]; 
        }

        public List<Empleados> ListarEmpleados()
        {
            List<Empleados> listaEmpleados = new List<Empleados>();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ListarEmpleados", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Empleados empleado = new Empleados()
                                {
                                    IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Dui = reader["Dui"].ToString(),
                                    Direccion = reader["Direccion"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    Fecha = Convert.ToDateTime(reader["Fecha"])
                                };
                                listaEmpleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return listaEmpleados;
        }

        public Empleados AgregarEmpleado(Empleados empleado)
        {
            Empleados saved = new Empleados();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_CrearEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Dui", empleado.Dui);
                    command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                    command.Parameters.AddWithValue("@Email", empleado.Email);
                    command.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                    command.Parameters.AddWithValue("@Estado", empleado.Estado);
                    command.Parameters.AddWithValue("@Patron", Patron); 

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                saved.IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
                                saved.Nombres = reader["Nombres"].ToString();
                                saved.Apellidos = reader["Apellidos"].ToString();
                                saved.Dui = reader["Dui"].ToString();
                                saved.Direccion = reader["Direccion"].ToString();
                                saved.Email = reader["Email"].ToString();
                                saved.Telefono = reader["Telefono"].ToString();
                                saved.Estado = Convert.ToBoolean(reader["Estado"]);
                                saved.Fecha = Convert.ToDateTime(reader["Fecha"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar empleado, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return saved;
        }

    }
}
