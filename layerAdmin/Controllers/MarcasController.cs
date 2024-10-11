using layerEntidad;
using layerNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layerAdmin.Controllers
{
    public class MarcasController : Controller
    {
        private CN_Marcas _marcaService;

        public MarcasController()
        {
            _marcaService = new CN_Marcas();
        }

        public ActionResult Index()
        {
            return View();
        }

        // POST: Marca/Crear
        [HttpPost]
        public JsonResult Crear(Marca oMarca)
        {
            ResultadoOperacion<Marca> resultado = _marcaService.AgregarMarca(oMarca); 

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    message = resultado.Mensaje
                });
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            });
        }

        // POST: Marca/Actualizar
        [HttpPost]
        public JsonResult Actualizar(Marca oMarca)
        {
            ResultadoOperacion<Marca> resultado = _marcaService.AgregarMarca(oMarca);

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    message = resultado.Mensaje
                });
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            });
        }

        // GET: Marca/Detalle 
        [HttpGet]
        public JsonResult Detalle(int id)
        {
            ResultadoOperacion<Marca> resultado = _marcaService.ObtenerMarcaPorId(id); 

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    data = resultado.Datos  
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje 
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Marca/Eliminar
        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            ResultadoOperacion<object> resultado = _marcaService.EliminarMarca(id);

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    message = resultado.Mensaje
                });
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            });
        }

        // GET: Marca/Listar
        [HttpGet]
        public JsonResult Listar()
        {
            ResultadoOperacion<List<Marca>> resultado = _marcaService.ListarMarcas();

            if (resultado.Exito)
            {
                return Json(new
                {
                    data = resultado.Datos  
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
