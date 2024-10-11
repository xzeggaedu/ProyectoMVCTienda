using layerEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using layerNegocio;


namespace layerAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios() 
        {
            List<Usuarios> oLista = new List<Usuarios>();
            oLista = new CN_Usuarios().ListarUsuarios();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IdUsuario == 0)
            {
                resultado = new CN_Usuarios().AgregarUsuario(obj, out mensaje);
            }
            else 
            {
                resultado = new CN_Usuarios().ModificarUsuario(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().EliminarUsuario(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}