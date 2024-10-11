using layerEntidad;
using layerNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layerAdmin.Controllers
{
    public class ProductosController : Controller
    {
        private CN_Productos _productoService;

        public ProductosController()
        {
            _productoService = new CN_Productos();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Crear(Productos oProducto)
        {
            ResultadoOperacion<Productos> resultado = _productoService.AgregarProducto(oProducto);

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    message = resultado.Mensaje,
                    data = resultado.Datos 
                });
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            });
        }


        [HttpPost]
        public JsonResult Actualizar(Productos oProducto)
        {
            ResultadoOperacion<Productos> resultado = _productoService.ActualizarProducto(oProducto);

            if (resultado.Exito)
            {
                return Json(new
                {
                    success = true,
                    message = resultado.Mensaje,
                    data = resultado.Datos 
                });
            }

            return Json(new
            {
                success = false,
                message = resultado.Mensaje
            });
        }


        [HttpGet]
        public JsonResult Detalle(int id)
        {
            ResultadoOperacion<Productos> resultado = _productoService.ObtenerProductoPorId(id);

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

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            ResultadoOperacion<object> resultado = _productoService.EliminarProducto(id);

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


        [HttpGet]
        public JsonResult Listar()
        {
            ResultadoOperacion<List<Productos>> resultado = _productoService.ListarProductos();

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
