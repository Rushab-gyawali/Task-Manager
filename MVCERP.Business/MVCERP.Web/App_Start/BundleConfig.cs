using System.Web.Optimization;

namespace MVCERP.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/MasterPageScript").Include("~/Script/master/js/modernizr.js"
                , "~/Scripts/master/js/bootstrap.min.js"
                , "~/Scripts/master/js/jquery.min.js"
                , "~/Scripts/master/js/jquery.slimscroll.js"
                , "~/Scripts/master/js/metisMenu.js"
                , "~/Scripts/master/js/core.js"
                , "~/Scripts/master/js/mediaquery.js"
                , "~/Scripts/master/js/equalize.js"
                , "~/Scripts/master/js/app.js"
                ));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/master/site.css"
               , "~/Content/master/css/bootstrap.min.css"
               , "~/Content/master/css/font-awesome.min.css"
               , "~/Content/master/css/metisMenu.css"
               , "~/Content/master/css/morris-0.4.3.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}