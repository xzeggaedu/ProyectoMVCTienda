using layerEntidad;
using layerNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layerAdmin.Controllers
{
    public class MantenimientosController : Controller
    {
        // GET: Mantenimientos
        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categorias> oLista = new List<Categorias>();
            oLista = new CN_Categorias().ListarCategorias();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categorias obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IdCategoria == 0)
            {
                resultado = new CN_Categorias().AgregarCategoria(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Categorias().ModificarCategoria(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categorias().EliminarCategoria(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}