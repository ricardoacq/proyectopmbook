using System.Web;
using System.Web.Optimization;

namespace MvcLogin
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Angularcss").Include(
                 "~/Content/Angular_Modules/angular-material/angular-material.css",
                 "~/Content/Angular_Modules/angular-material-data-table/md-data-table.css",
                 "~/Content/General/font-awesome.css",
                  "~/Content/Bootstrap/bootstrap.css",
                  "~/Content/simple-sidebar.css",
                  "~/Content/sweetalert2.css",
                  "~/Content/General/IconFix.css",
				  "~/Content/Angular_Modules/angular-material-data-table/md-data-table.css",
                  "~/Content/ng-tags-input.css",
				  "~/Content/Angular_Modules/angular-ui-layout-splitter/ui-layout.css",
                  "~/Content/fileinput.css",
                  "~/Content/ui-grid.css",
                  "~/Content/ng-grid.css"
                ));
            bundles.Add(new StyleBundle("~/Content/MaterialIcons", "https://fonts.googleapis.com/icon?family=Material+Icons").Include());

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                    "~/Content/General/style.css"
                ));

            bundles.Add(new StyleBundle("~/Content/error123").Include(
                    "~/Content/Error/css/style.css"
                ));

        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.js",
                         "~/Scripts/Layouts/_Layout.js"
                        
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.5.3"));


            //Angular Material
            bundles.Add(new ScriptBundle("~/bundles/Angular").Include(
                        "~/Content/Angular_Modules/angular/angular.js",
                        "~/Content/Angular_Modules/angular-aria/angular-aria.js",
                        "~/Content/Angular_Modules/angular-animate/angular-animate.js",
                        "~/Content/Angular_Modules/angular-material/angular-material.js",
                        "~/Content/Angular_Modules/angular/angular-locale_es-mx.js",
						"~/Content/Angular_Modules/angular-material-data-table/md-data-table.js",
						"~/Content/Angular_Modules/angular-messages/angular-messages.js",
						"~/Content/Angular_Modules/angular-mocks/angular-mocks.js",
						"~/Content/Angular_Modules/angular-route/angular-route.js",
						"~/Content/Angular_Modules/angular-sanitize/angular-sanitize.js",
						"~/Content/Angular_Modules/angular-ui-layout-splitter/ui-layout.js",
						"~/Content/Angular_Modules/angular-ui-layout-splitter/index.js"
						));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapJS").Include(
                   "~/Scripts/bootstrap.min.js",
                   "~/Scripts/sweetalert2.js",
                   "~/Scripts/ng-tags-input.js",
                   "~/Scripts/ng-file-upload.js",
                   "~/Scripts/fileinput.js",
                   "~/Scripts/canvasjs.js",
                   "~/Scripts/ui-grid.js",
                   //"~/Scripts/ng-grid.js",
                   "~/Scripts/moment.js"
                   ));
            //Areas
            bundles.Add(new ScriptBundle("~/bundles/Areas/Admin").Include(
                       "~/Scripts/Areas/Admin/app.js",
                        "~/Scripts/Areas/Ejecucion/Ejecucion.js"
                    ));
        } 

        
    }
}