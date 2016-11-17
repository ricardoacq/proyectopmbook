using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;
using MvcLogin.Linq;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class ActividadesController : Controller
    {
        //
        // GET: /Ejecucion/Actividades/

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
                Actividades = Result.Actividades,
                bError    = Result.bError,
                msgErr    = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerActividades_Grid(string nFechaInicial, string nFechaFinal ,int idProyecto,int idModulo,int idComponente, int idConsultor)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividades_Grid(DB, nFechaInicial, nFechaFinal,  idProyecto, idModulo, idComponente,  idConsultor);

            return Json(new
            {
                Actividades_Grid = Result.Actividades_Grid,
                nUsuarioLogin=Result.nUsuarioLogin,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerActividadesInActivas_Grid(string nFechaInicial, string nFechaFinal, int idProyecto,int idModulo,int idComponente, int idConsultor)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerActividadesInActivas_Grid(DB, nFechaInicial, nFechaFinal, idProyecto, idModulo, idComponente, idConsultor);

            return Json(new
            {
                Actividades_Grid = Result.Actividades_Grid,
                bError = Result.bError,
                msgErr = Result.msgErr
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
        public ActionResult ObtenerImpactoAyuda()
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerImpactoAyuda(DB);

            return Json(new
            {
                Impacto = Result.Impacto,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerAcciones(int idActividad)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerAcciones(DB,idActividad);

            return Json(new
            {
                Accion = Result.Accion,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerProyectos()
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerProyectos(DB);

            return Json(new
            {
                Proyecto = Result.Proyecto,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerProyectosInactivos()
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerProyectosInactivos(DB);

            return Json(new
            {
                Proyecto = Result.Proyecto,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerModulos(int idProyecto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerModulos(DB,idProyecto);

            return Json(new
            {
                ProyectoModulos = Result.ProyectoModulos,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerComponentes(int idModulo)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerComponentes(DB, idModulo);

            return Json(new
            {
                ProyectoComponentes = Result.ProyectoComponentes,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerHistorial(int idActividad,int idConsultor)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.ObtenerHistorial(DB,idActividad,idConsultor);

            return Json(new
            {
                Historial = Result.Historial,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarReporteAvanceActividad(DTO_Actividad_Grid RegistroTrabajo, string Comentario, int Horas, double Minutos, int HorasRes, double MinutosRes)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.GuardarReporteAvanceActividad( DB,RegistroTrabajo,Comentario, Horas, Minutos, HorasRes, MinutosRes);           

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr,
                result=Result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarAccion(int idActividad,string Accion)
        {
            PMBookDataContext DB = new PMBookDataContext();
            ActividadesModel model = new ActividadesModel();
            DTO_Actividad_Result Result = model.GuardarAccion(DB, idActividad,Accion);

            return Json(new
            {
                bError = Result.bError,
                msgErr = Result.msgErr,
                result = Result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
