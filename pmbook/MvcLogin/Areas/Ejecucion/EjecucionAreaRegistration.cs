using System.Web.Mvc;

namespace MvcLogin.Areas.Ejecucion
{
    public class EjecucionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Ejecucion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Ejecucion_default",
                "Ejecucion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
