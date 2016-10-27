using MvcLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcLogin.Helpers
{
    class FilterSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var enableSecurity = ConfigurationManager.AppSettings[ViewConstants.ENABLE_SECURITY];

            DataSesion Sesion = (DataSesion)HttpContext.Current.Session["DataSesion"];
            String Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            String Action = filterContext.ActionDescriptor.ActionName;
            if (Sesion.bSesionActiva)
            {
                if (Controller == "User" && Action == "logIn")
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", Action);
                    redirectTargetDictionary.Add("controller",Controller);
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            else
            {
                if (Controller != "User" && Action != "logIn")
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "logIn");
                    redirectTargetDictionary.Add("controller", "User");
                    redirectTargetDictionary.Add("area","");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
        }
    }
}