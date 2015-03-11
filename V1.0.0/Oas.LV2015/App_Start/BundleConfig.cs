using System.Web;
using System.Web.Optimization;

namespace Oas.LV2015
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/angular-ui-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/app/app.js",
                    "~/app/shared/directives/wcOverlay.js",
                    "~/app/shared/directives/menuHighlighter.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/app/assets/css/bootstrap.css",
                      "~/app/assets/css/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
