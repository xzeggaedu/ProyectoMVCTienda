using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using layerEntidad;
namespace layerDatos
{
    public class CD_Productos
    {
        private SqlConnection connection;

        public CD_Productos()
        {
            connection = new SqlConnection(Conexion.cn);
        }

        public List<Productos> ListarProductos()
        {
            List<Productos> listaProductos = new List<Productos>();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ListarProductos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Productos producto = new Productos()
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    NombreProducto = reader["NombreProducto"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    oMarca = new Marca()
                                    {
                                        IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                        NombreMarca = reader["NombreMarca"].ToString(),
                                        Estado = Convert.ToBoolean(reader["EstadoMarca"]),
                                        Fecha = Convert.ToDateTime(reader["FechaMarca"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                    },
                                    oCategoria = new Categorias()
                                    {
                                        IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                        DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                        Estado = Convert.ToBoolean(reader["EstadoCategoria"]),
                                        Fecha = Convert.ToDateTime(reader["FechaCategoria"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                    },
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToSingle(reader["Stock"]),
                                    RutaImagen = reader["RutaImagen"].ToString(),
                                    NombreImagen = reader["NombreImagen"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                };
                                listaProductos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return listaProductos;
        }


        public Productos ObtenerProductoPorId(int id)
        {
            Productos oProducto = new Productos();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_ObtenerProductoPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                oProducto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                                oProducto.NombreProducto = reader["NombreProducto"].ToString();
                                oProducto.Descripcion = reader["Descripcion"].ToString();
                                oProducto.oMarca = new Marca()
                                {
                                    IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                    NombreMarca = reader["NombreMarca"].ToString(),
                                    Estado = Convert.ToBoolean(reader["EstadoMarca"]),
                                    Fecha = Convert.ToDateTime(reader["FechaMarca"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                };
                                oProducto.oCategoria = new Categorias()
                                {
                                    IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                    DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                    Estado = Convert.ToBoolean(reader["EstadoCategoria"]),
                                    Fecha = Convert.ToDateTime(reader["FechaCategoria"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                };
                                oProducto.Precio = Convert.ToDecimal(reader["Precio"]);
                                oProducto.Stock = Convert.ToSingle(reader["Stock"]);
                                oProducto.RutaImagen = reader["RutaImagen"].ToString();
                                oProducto.NombreImagen = reader["NombreImagen"].ToString();
                                oProducto.Estado = Convert.ToBoolean(reader["Estado"]);
                                oProducto.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]).ToString("yyyy-MM-dd HH:mm:ss"); // Ajusta el formato según sea necesario
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto por id, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return oProducto;
        }

        public Productos AgregarProducto(Productos oProducto)
        {
            Productos saved = new Productos();
            try
            {
                using (SqlCommand command = new SqlCommand("SP_CrearProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreProducto", oProducto.NombreProducto);
                    command.Parameters.AddWithValue("@Descripcion", oProducto.Descripcion);
                    command.Parameters.AddWithValue("@IdMarca", oProducto.oMarca.IdMarca);
                    command.Parameters.AddWithValue("@IdCategoria", oProducto.oCategoria.IdCategoria);
                    command.Parameters.AddWithValue("@Precio", oProducto.Precio);
                    command.Parameters.AddWithValue("@Stock", oProducto.Stock);
                    command.Parameters.AddWithValue("@RutaImagen", oProducto.RutaImagen);
                    command.Parameters.AddWithValue("@NombreImagen", oProducto.NombreImagen);
                    command.Parameters.AddWithValue("@Estado", oProducto.Estado);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                saved.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                                saved.NombreProducto = reader["NombreProducto"].ToString();
                                saved.Descripcion = reader["Descripcion"].ToString();
                                saved.oMarca = new Marca()
                                {
                                    IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                    NombreMarca = reader["NombreMarca"].ToString(),
                                    Estado = Convert.ToBoolean(reader["EstadoMarca"]),
                                    Fecha = Convert.ToDateTime(reader["FechaMarca"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                };
                                saved.oCategoria = new Categorias()
                                {
                                    IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                    DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                    Estado = Convert.ToBoolean(reader["EstadoCategoria"]),
                                    Fecha = Convert.ToDateTime(reader["FechaCategoria"]).ToString("yyyy-MM-dd HH:mm:ss") // Ajusta el formato según sea necesario
                                };
                                saved.Precio = Convert.ToDecimal(reader["Precio"]);
                                saved.Stock = Convert.ToSingle(reader["Stock"]);
                                saved.RutaImagen = reader["RutaImagen"].ToString();
                                saved.NombreImagen = reader["NombreImagen"].ToString();
                                saved.Estado = Convert.ToBoolean(reader["Estado"]);
                                saved.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]).ToString("yyyy-MM-dd HH:mm:ss"); // Ajusta el formato según sea necesario
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return saved;
        }

        public void ActualizarProducto(Productos oProducto)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_ActualizarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", oProducto.IdProducto);
                    command.Parameters.AddWithValue("@NombreProducto", oProducto.NombreProducto);
                    command.Parameters.AddWithValue("@Descripcion", oProducto.Descripcion);
                    command.Parameters.AddWithValue("@IdMarca", oProducto.oMarca.IdMarca);
                    command.Parameters.AddWithValue("@IdCategoria", oProducto.oCategoria.IdCategoria);
                    command.Parameters.AddWithValue("@Precio", oProducto.Precio);
                    command.Parameters.AddWithValue("@Stock", oProducto.Stock);
                    command.Parameters.AddWithValue("@RutaImagen", oProducto.RutaImagen);
                    command.Parameters.AddWithValue("@NombreImagen", oProducto.NombreImagen);
                    command.Parameters.AddWithValue("@Estado", oProducto.Estado);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void EliminarProducto(int id)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_EliminarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", id);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto por id, " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}