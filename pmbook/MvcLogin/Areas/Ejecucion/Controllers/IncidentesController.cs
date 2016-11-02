using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;
using MvcLogin.Linq;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class IncidentesController : Controller
    {
        //
        // GET: /Ejecucion/Actividades/

        //public ActionResult Index()
        //{
        //    IncidentesModel model = new IncidentesModel();
        //    return View("Index", model);
        //}

        //public ActionResult ObtenerProductos(int nActividad)
        //{
        //    PMBookDataContext DB = new PMBookDataContext();
        //    ActividadesModel model = new ActividadesModel();
        //    DTO_Actividad_Result Result = model.ObtenerActividad(DB, nActividad);

        //    return Json(new
        //    {
        //        Actividad = Result.Actividad,
        //        bError = Result.bError,
        //        msgErr = Result.msgErr
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ObtenerProductos()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerProductos(DB);

            return Json(new
            {
                Productos = Result.Productos,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        ///
        public ActionResult ObtenerModulos(int idProducto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerModulos(DB,idProducto);

            return Json(new
            {
                Modulos = Result.Modulos,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        ///
        public ActionResult ObtenerComponentes(int idModulo)
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerComponentes(DB, idModulo);

            return Json(new
            {
                Componentes = Result.Componentes,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        ///
        public ActionResult ObtenerActividades_Grid(int nFechaInicial, int nFechaFinal)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividades_Grid(DB, nFechaInicial, nFechaFinal);

            return Json(new
            {
                Actividades_Grid = Result.Actividades_Grid,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerActividadesInActivas_Grid(int nFechaInicial, int nFechaFinal)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividadesInActivas_Grid(DB, nFechaInicial, nFechaFinal);

            return Json(new
            {
                Actividades_Grid = Result.Actividades_Grid,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarActividad(int nActividad)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.EliminarActividad(DB, nActividad);

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarActividad(DTO_Actividad Actividad)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.GuardarActividad(DB, Actividad);
            int id = Result.Actividad == null ? 0 : Result.Actividad.nActividad;

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr,
                nActividad = id
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
