using MvcLogin.Areas.Ejecucion.Models;
using System.Web.Mvc;

namespace MvcLogin.Areas.Ejecucion.Controllers
{
    public class EjecucionController : Controller
    {
        //
        // GET: /Ejecucion/Ejecucion/

        public ActionResult Index()
        {
            EjecucionModel model = new EjecucionModel();
            return View("Index", model);
        }

    }
}
