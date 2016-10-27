using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;
using MvcLogin.Linq;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class ProyectosController : Controller
    {
        //
        // GET: /Ejecucion/Proyectos/

        public ActionResult Index()
        {
            ProyectosModel model = new ProyectosModel();
            return View("Index", model);
        }

        public ActionResult ObtenerProyecto(int nProyecto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ProyectosModel model = new ProyectosModel();
            DTO_Proyectos_Result Result = model.ObtenerProyecto(DB, nProyecto);

            return Json(new
            {
                Proyecto = Result.Proyecto,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerProyectos(bool MostrarInactivos)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ProyectosModel model = new ProyectosModel();
            DTO_Proyectos_Result Result = model.ObtenerProyectos(DB, MostrarInactivos);

            return Json(new
            {
                Proyecto = Result.Proyecto,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProyecto(int nProyecto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ProyectosModel model = new ProyectosModel();
            DTO_Proyectos_Result Result = model.EliminarProyecto(DB, nProyecto);

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarProyecto(DTO_Proyecto Proyecto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ProyectosModel model = new ProyectosModel();
            DTO_Proyectos_Result Result = model.GuardarProyecto(DB, Proyecto);
            int id = Result.Proyecto == null ? 0 : Result.Proyecto.nProyecto;

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr,
                nProyecto = id
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

