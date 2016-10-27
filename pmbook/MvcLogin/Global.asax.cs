using MvcLogin.Helpers;
using MvcLogin.Helpers.Sesion;
using MvcLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcLogin
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new FilterSession());
        }

        //Variables de sesion
        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("DataSesion", new DataSesion());
            HttpContext.Current.Session.Add("DataNav", new List<DataArea>());
        }
        //Limpiar sesion
        protected void Session_End(Object sender, EventArgs e)
        {
            //HttpContext.Current.Session["Sesion"] = new Sesion();
        }


        //void Application_End(object sender, EventArgs e)
        //{
        //    //  Código que se ejecuta cuando se cierra la aplicación

        //}

        //void Application_Error(object sender, EventArgs e)
        //{
           
        //    Exception exception = Server.GetLastError();
        //    System.Diagnostics.Debug.WriteLine(exception);
        //    Response.Redirect("/User/Error");
        //    // Código que se ejecuta al producirse un error no controlado

        //}

        
    }
}