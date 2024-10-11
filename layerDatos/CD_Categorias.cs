using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using layerEntidad;
using System.Data;
using System.Data.SqlClient;

namespace layerDatos
{
    public class CD_Categorias
    {
        DataSet ds = new DataSet();
        //Guardar el patron en el web.config
        string Patron = "D153ño";
        public List<Categorias> ListarCategorias()
        {
            List<Categorias> listaCate = new List<Categorias>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_BuscarCategorias", Conexion.cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "Categorias");
                //Llenar la lista con los datos devueltos por el SP
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                listaCate.Add(
                                    new Categorias()
                                    {
                                        IdCategoria = Convert.ToInt32(ds.Tables[0].Rows[i]["IdCategoria"]),
                                        DescripcionCategoria = ds.Tables[0].Rows[i]["DescripcionCategoria"].ToString(),
                                        Estado = Convert.ToBoolean(ds.Tables[0].Rows[i]["Estado"]),
                                        Fecha = ds.Tables[0].Rows[i]["Fecha"].ToString()
                                    }
                                );
                            }
                        }
                        else
                        {
                            listaCate = new List<Categorias>();
                            listaCate.Add(
                                new Categorias()
                                {
                                    IdCategoria = -1,
                                    Error = "Sin registros"
                                }
                            );
                        }

                    }
                    else
                    {
                        listaCate = new List<Categorias>();
                        listaCate.Add(
                            new Categorias()
                            {
                                IdCategoria = -1,
                                Error = "DataTable vacío"
                            }
                        );
                    }
                }
                else
                {
                    listaCate = new List<Categorias>();
                    listaCate.Add(
                        new Categorias()
                        {
                            IdCategoria = -1,
                            Error = "DataSet nulo"
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                listaCate = new List<Categorias>();
                listaCate.Add(
                    new Categorias()
                    {
                        IdCategoria = -1,
                        Error = ex.Message
                    }
                );
            }

            return listaCate;
        }

        public int AgregarCategoria(Categorias obj, out string Mensaje)
        {
            int IdCate = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_AgregarCategoria", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DescripcionCategoria", obj.DescripcionCategoria);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    conex.Open();
                    cmd.ExecuteNonQuery();//Ejecuta el SP en la BD
                    IdCate = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdCate = 0;
                Mensaje = ex.Message;
            }
            return IdCate;
        }

        public bool ModificarCategoria(Categorias obj, out string Mensaje)
        {
            bool Resp = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarCategoria", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@DescripcionCategoria", obj.DescripcionCategoria);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    conex.Open();
                    //cmd.ExecuteNonQuery();//Ejecuta el SP en la BD
                    Resp = cmd.ExecuteNonQuery() > 0 ? true : false; //Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resp = false;
                Mensaje = ex.Message;
            }
            return Resp;
        }

        public bool EliminarCategoria(int IdCate, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarCategoria", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", IdCate);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    conex.Open();
                    cmd.ExecuteNonQuery();//Ejecuta el SP en la BD
                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }
    }
}
