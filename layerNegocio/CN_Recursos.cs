using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Net.Mail;

namespace layerNegocio
{
    public class CN_Recursos
    {
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("carritocompras850@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("carritocompras850@gmail.com", "kzaswzscsxneubcv"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex) 
            {
                resultado = false;
            }
            return resultado;
        }

        public static string GenerarClave()
        { 
            //Generar claves aleatorias de 6 caracteres
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }

    }
}
