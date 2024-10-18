using layerEntidad;
using layerNegocio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace layerAdmin.Controllers
{
    public class EmpleadosController : Controller
    {
        private CN_Empleados _empleadoService;

        public EmpleadosController()
        {
            _empleadoService = new CN_Empleados();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Crear(Empleados oEmpleado)
        {
            ResultadoOperacion<Empleados> resultado = _empleadoService.AgregarEmpleado(oEmpleado);

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
        public JsonResult Listar()
        {
            ResultadoOperacion<List<Empleados>> resultado = _empleadoService.ListarEmpleados();

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
