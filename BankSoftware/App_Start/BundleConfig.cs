using System.Web;
using System.Web.Optimization;

namespace BankSoftware
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/template/jquery-1.9.1.min.js",
                        "~/Scripts/template/jquery-ui-1.10.1.custom.min.js",
                        "~/Scripts/template/flot/jquery.flot.js"
                        //,
                        //"~/Scripts/template/flot/jquery.flot.resize.js",
                        //"~/Scripts/template/datatables/jquery.dataTables.js",
                        //"~/Scripts/template/common.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));


            bundles.Add(new ScriptBundle("~/bundles/templatebootstrap").Include(
                      "~/Content/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/ui-grid.js",
                        "~/Scripts/bank.js",
                        //"~/Scripts/sweetAlert/sweetalert2.all.min.js",
                        //"~/Scripts/sweetAlert/sweetalert2.js",

                        //"~/Scripts/sweetAlert/sweetalert2.all.min.js",
                        "~/Scripts/select2.js"
                        ));

            bundles.Add(new Bundle("~/bundles/bank_controllers").Include(
                "~/Scripts/bankScript/ctrl-bank.js",
                "~/Scripts/bankScript/ctrl-user.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/ui-grid.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/templatecss").Include(
                     "~/Content/bootstrap/css/bootstrap.min.css",
                     "~/Content/bootstrap/css/bootstrap-responsive.min.css",
                     "~/Content/css/theme.css",
                      "~/Content/ui-grid.css",
                      "~/Content/site.css",
                    "~/images/icons/css/font-awesome.css"));
        }
    }
}
