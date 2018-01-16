using System.Web;
using System.Web.Optimization;

namespace DOH7PAYROLL
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/public/bootstrap/index").Include(
                   "~/public/assets/js/jquery.min.js",
                   "~/public/assets/js/bootstrap.min.js"));


            bundles.Add(new StyleBundle("~/public/bootstrap/index").Include(
                "~/public/assets/css/bootstrap.min.css",
                 "~/public/assets/css/font-awesome.min.css",
                 "~/public/assets/css/AdminLTE.min.css"));

            bundles.Add(new ScriptBundle("~/public/bootstrap/home").Include(
                 "~/public/assets/js/jquery.min.js",
                 "~/public/assets/js/bootstrap.min.js",
                 "~/public/plugin/daterangepicker/moment.min.js",
                 "~/public/plugin/daterangepicker/daterangepicker.js",
                 "~/public/plugin/Lobibox old/Lobibox.js",
                 "~/public/assets/js/jquery-validate.js",
                 "~/public/assets/js/bootstrap.min.js",
                 "~/public/assets/datepicer/js/bootstrap-datepicker.js",
                 "~/public/plugin/clockpicker/dist/jquery-clockpicker.min.js",
                 "~/public/plugin/clockpicker/dist/bootstrap-clockpicker.min.js",
                 "~/public/assets/js/ie10-viewport-bug-workaround.js",
                 "~/public/assets/js/script.js",
                 "~/public/assets/js/form-justification.js",
                 "~/public/plugin/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                 "~/public/plugin/select2/select2.full.min.js",
                 "~/public/plugin/chosen/chosen.jquery.js",
                 "~/public/plugin/fullcalendar/moment.js",
                 "~/public/plugin/fullcalendar/fullcalendar.min.js",
                 "~/public/plugin/iCheck/icheck.min.js"));


            bundles.Add(new StyleBundle("~/public/bootstrap/home").Include(
                "~/public/assets/css/bootstrap.min.css",
                 "~/public/assets/css/bootstrap-theme.min.css",
                 "~/public/assets/css/font-awesome.min.css",
                 "~/public/assets/css/AdminLTE.min.css",
                 "~/public/plugin/chosen/chosen.css",
                 "~/public/assets/css/ie10-viewport-bug-workaround.css",
                 "~/public/assets/css/style.css",
                 "~/public/plugin/Lobibox old/lobibox.css",
                 "~/public/plugin/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                 "~/public/plugin/daterangepicker/daterangepicker-bs3.css",
                 "~/public/plugin/clockpicker/dist/jquery-clockpicker.min.css",
                 "~/public/plugin/clockpicker/dist/bootstrap-clockpicker.min.css",
                 "~/public/assets/datepicer/css/bootstrap-datepicker3.css",
                 "~/public/assets/datepicer/css/bootstrap-datepicker3.standalone.css"));
        }
    }
}
