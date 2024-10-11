using layerEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layerDatos
{
    public class CD_Marcas
    {
        private SqlConnection connection;

        public CD_Marcas()
        {
            connection = new SqlConnection(Conexion.cn);
        }

        public List<Marca> ListarMarcas()
        {
            List<Marca> listaMarcas = new List<Marca>();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ListarMarcas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listaMarcas.Add(
                                    new Marca()
                                    {
                                        IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                        NombreMarca = reader["NombreMarca"].ToString(),
                                        Estado = Convert.ToBoolean(reader["Estado"]),
                                        Fecha = reader["Fecha"].ToString()
                                    }
                                );
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar marcas, " + ex.Message);
            }
            finally { connection.Close(); }

            return listaMarcas;

        }

        public Marca ObtenerMarcaPorId(int id)
        {
            Marca oMarca = new Marca();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_ObtenerMarcaPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdMarca", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                oMarca.IdMarca = Convert.ToInt32(reader["IdMarca"]);
                                oMarca.NombreMarca = reader["NombreMarca"].ToString();
                                oMarca.Estado = Convert.ToBoolean(reader["Estado"]);
                                oMarca.Fecha = reader["Fecha"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener marca por id, " + ex.Message);
            }
            finally { connection.Close(); }

            return oMarca;
        }

        public Marca AgregarMarca(Marca oMarca)
        {
            Marca saved = new Marca();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_CrearMarca", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreMarca", oMarca.NombreMarca);
                    command.Parameters.AddWithValue("@Estado", oMarca.Estado);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                saved.IdMarca = Convert.ToInt32(reader["IdMarca"]);
                                saved.NombreMarca = reader["NombreMarca"].ToString();
                                saved.Estado = Convert.ToBoolean(reader["Estado"]);
                                saved.Fecha = reader["Fecha"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar marca, " + ex.Message);
            }
            finally { connection.Close(); }

            return saved;
        }

        public void ActualizarMarca(Marca oMarca)
        {           

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ActualizarMarca", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdMarca", oMarca.IdMarca);
                    command.Parameters.AddWithValue("@NombreMarca", oMarca.NombreMarca);
                    command.Parameters.AddWithValue("@Estado", oMarca.Estado);

                    connection.Open();

                   command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar marca, " + ex.Message);
            }
            finally { connection.Close(); }

        }

        public void EliminarMarca(int id)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_EliminarMarca", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdMarca", id);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar marca por id, " + ex.Message);
            }
            finally { connection.Close(); }
        }

    }
}
