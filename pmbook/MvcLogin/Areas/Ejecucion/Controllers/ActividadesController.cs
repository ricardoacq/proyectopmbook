using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;
using MvcLogin.Linq;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class ActividadesController : Controller
    {
        //
        // GET: /Configuracion/Actividades/

        public ActionResult Index()
        {
            ActividadesModel model = new ActividadesModel();
            return View("Index", model);
        }

        public ActionResult ObtenerActividad(int nActividad)
        {
            PMBookDataContext DB        = new PMBookDataContext();
            ActividadesModel model      = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividad(DB, nActividad);

            return Json(new
            {
                Actividad = Result.Actividad,
                bError    = Result.bError,
                msgErr    = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerActividades(int nFechaInicial, int nFechaFinal)
        {
            PMBookDataContext DB        = new PMBookDataContext();
            ActividadesModel model      = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividades(DB, nFechaInicial, nFechaFinal);

            return Json(new
            {
                Actividad = Result.Actividad,
                bError    = Result.bError,
                msgErr    = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarActividad(int nActividad)
        {
            PMBookDataContext DB        = new PMBookDataContext();
            ActividadesModel model      = new ActividadesModel();
            DTO_Actividad_Result Result = model.EliminarActividad(DB, nActividad);

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarActividad(DTO_Actividad Actividad)
        {
            PMBookDataContext DB        = new PMBookDataContext();
            ActividadesModel model      = new ActividadesModel();
            DTO_Actividad_Result Result = model.GuardarActividad(DB, Actividad);
            int id = Result.Actividad == null ? 0 : Result.Actividad.nActividad;

            return Json(new
            {
                bError     = Result.bError,
                msgErr     = Result.msgErr,
                nActividad = id
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
