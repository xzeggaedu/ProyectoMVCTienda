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
    public class CD_Usuarios
    {
        DataSet ds = new DataSet();
        //Guardar el patron en el web.config
        string Patron = "D153ño";
        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> listaUsers = new List<Usuarios>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_BuscarUsuarios", Conexion.cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "Usuarios");
                //Llenar la lista con los datos devueltos por el SP
                if (ds != null)
                {
                    if (ds.Tables.Count > 0) 
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                listaUsers.Add(
                                    new Usuarios()
                                    {
                                        IdUsuario = Convert.ToInt32(ds.Tables[0].Rows[i]["IdUsuario"]),
                                        Nombres = ds.Tables[0].Rows[i]["Nombres"].ToString(),
                                        Apellidos = ds.Tables[0].Rows[i]["Apellidos"].ToString(),
                                        Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                                        Password = ds.Tables[0].Rows[i]["Password"].ToString(),
                                        Reestablecer = Convert.ToBoolean(ds.Tables[0].Rows[i]["Reestablecer"]),
                                        Estado = Convert.ToBoolean(ds.Tables[0].Rows[i]["Estado"]),
                                        Fecha = ds.Tables[0].Rows[i]["Fecha"].ToString()
                                    }
                                );
                            }
                        }
                        else
                        {
                            listaUsers = new List<Usuarios>();
                            listaUsers.Add(
                                new Usuarios()
                                {
                                    IdUsuario = -1,
                                    Error = "Sin registros"
                                }
                            );
                        }
                            
                    }
                    else
                    {
                        listaUsers = new List<Usuarios>();
                        listaUsers.Add(
                            new Usuarios()
                            {
                                IdUsuario = -1,
                                Error = "DataTable vacío"
                            }
                        );
                    }
                }
                else
                {
                    listaUsers = new List<Usuarios>();
                    listaUsers.Add(
                        new Usuarios()
                        {
                            IdUsuario = -1,
                            Error = "DataSet nulo"
                        }
                    );
                }
            }
            catch (Exception ex) 
            {
                listaUsers = new List<Usuarios>();
                listaUsers.Add(
                    new Usuarios()
                    {
                        IdUsuario = -1,
                        Error = ex.Message
                    }
                );
            }

            return listaUsers;
        }

        public int AgregarUsuario(Usuarios obj, out string Mensaje)
        {
            int IdUser = 0; 
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                { 
                    SqlCommand cmd = new SqlCommand("SP_AgregarUsuario", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Reestablecer", obj.Reestablecer);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@Patron", Patron);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    conex.Open();
                    cmd.ExecuteNonQuery();//Ejecuta el SP en la BD
                    IdUser = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex) 
            {
                IdUser = 0;
                Mensaje = ex.Message;
            }
            return IdUser;
        }

        public bool ModificarUsuario(Usuarios obj, out string Mensaje)
        {
            bool Resp = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarUsuario", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Reestablecer", obj.Reestablecer);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@Patron", Patron);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        public bool EliminarUsuario(int IdUser, out string Mensaje)
        {
            bool Resp = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarUsuario", conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUser);
                    //cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    conex.Open();
                    Resp = cmd.ExecuteNonQuery() > 0 ? true : false;//Ejecuta el SP en la BD
                    //Resp = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    //Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resp = false;
                Mensaje = ex.Message;
            }
            return Resp;
        }
    }
}
