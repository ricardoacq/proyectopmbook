using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;
using MvcLogin.Linq;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class IncidentesController : Controller
    {
        
        // GET: /Ejecucion/Actividades/

        public ActionResult Index()
        {
            IncidentesModel model = new IncidentesModel();
            return View("Index", model);
        }

        public ActionResult ObtenerIncidentes(int idCliente)
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerIncidentes(DB, idCliente);

            return Json(new
            {
                Incidentes = Result.Incidentes,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerIncidentesInactivos(int idCliente)
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerIncidentesInactivos(DB, idCliente);

            return Json(new
            {
                Incidentes = Result.Incidentes,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE PRODUCTOS
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
        ///TRAE TIPO INCIDENTE
        public ActionResult ObtenerTipoIncidente()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerTipoIncidente(DB);

            return Json(new
            {
                TipoIncidente = Result.TipoIncidente,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE VERSIONES
        public ActionResult ObtenerVersiones(int idProducto)
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerVersiones(DB, idProducto);

            return Json(new
            {
                Versiones = Result.Versiones,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE MODULOS
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

        ///TRAE  COMPONENTES
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

        ///TRAE CLIENTES
        public ActionResult ObtenerClientes()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerClientes(DB);

            return Json(new
            {
                Clientes = Result.Clientes,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE LIDERES
        public ActionResult ObtenerLideres()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerLideres(DB);

            return Json(new
            {
                Lideres = Result.Lideres,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE  TESTERS
        public ActionResult ObtenerTesters()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerTesters(DB);

            return Json(new
            {
                Testers = Result.Testers,
                bError = Result.bError,
                msgErr = Result.msgErr
            }, JsonRequestBehavior.AllowGet);
        }

        ///TRAE CONSULTORES
        public ActionResult ObtenerConsultores()
        {
            PMBookDataContext DB = new PMBookDataContext();
            IncidentesModel model = new IncidentesModel();
            DTO_Incidente_Result Result = model.ObtenerConsultores(DB);

            return Json(new
            {
                Consultores = Result.Consultores,
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
