using layerDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using layerEntidad;
using System.Security.Permissions;

namespace layerNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDatos = new CD_Usuarios();

        public List<Usuarios> ListarUsuarios() 
        {
            return objCapaDatos.ListarUsuarios();
        }

        public int AgregarUsuario(Usuarios obj, out string Mensaje) 
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre de usuario no puede quedar vacío!";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido de usuario no puede quedar vacío!";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El correo de usuario no puede quedar vacío!";
            }

            if (string.IsNullOrEmpty(Mensaje)) 
            {
                string Clave = CN_Recursos.GenerarClave();
                obj.Password = Clave;
                obj.Reestablecer = true;
                string asunto = "Creación de Cuenta - Carrito Compras Empresa X";
                string mensajeCorreo = "<h3>Su cuenta fue creada correctamente!</h3><br><p>Su contraseña para acceder a la aplicación es: !clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("!clave!", Clave);

                bool respuesta =  CN_Recursos.EnviarCorreo(obj.Email, asunto, mensajeCorreo);

                if (respuesta)
                {
                    return objCapaDatos.AgregarUsuario(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "Error. No se pudo enviar el correo de alta del usuario";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool ModificarUsuario(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre de usuario no puede quedar vacío!";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido de usuario no puede quedar vacío!";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El correo de usuario no puede quedar vacío!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDatos.ModificarUsuario(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool EliminarUsuario(int IdUser, out string Mensaje)
        {
            
            return objCapaDatos.EliminarUsuario(IdUser, out Mensaje);

        }
    }
}
