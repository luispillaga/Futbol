using System.Web;
using System.Web.Optimization;

namespace Futbol
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/vendor/jquery-1.12.4.min.js",
                      "~/Content/js/bootstrap.min.js",
                      "~/Content/js/wow.min.js",
                      "~/Content/js/jquery-price-slider.js",
                      "~/Content/js/jquery.meanmenu.js",
                      "~/Content/js/owl.carousel.min.js",
                      "~/Content/js/jquery.sticky.js",
                      "~/Content/js/jquery.scrollUp.min.js",
                      "~/Content/js/counterup/jquery.counterup.min.js",
                      "~/Content/js/counterup/waypoints.min.js",
                      "~/Content/js/counterup/counterup-active.js",
                      "~/Content/js/scrollbar/jquery.mCustomScrollbar.concat.min.js",
                      "~/Content/js/scrollbar/mCustomScrollbar-active.js",
                      "~/Content/js/metisMenu/metisMenu.min.js",
                      "~/Content/js/metisMenu/metisMenu-active.js",
                      "~/Content/js/morrisjs/raphael-min.js",
                      "~/Content/js/morrisjs/morris.js",
                      "~/Content/js/morrisjs/morris-active.js",
                      "~/Content/js/sparkline/jquery.sparkline.min.js",
                      "~/Content/js/sparkline/jquery.charts-sparkline.js",
                      "~/Content/js/sparkline/sparkline-active.js",
                      "~/Content/js/calendar/moment.min.js",
                      "~/Content/js/calendar/fullcalendar.min.js",
                      "~/Content/js/calendar/fullcalendar-active.js",
                      "~/Content/js/plugins.js",
                      "~/Content/js/main.js",
                      "~/Content/js/tawk-chat.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/datatables/jquery.datatables.js",
                      "~/Scripts/datatables/datatables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/owl.theme.css",
                      "~/Content/css/owl.transitions.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/normalize.css",
                      "~/Content/css/meanmenu.min.css",
                      "~/Content/css/main.css",
                      "~/Content/css/educate-custon-icon.css",
                      "~/Content/css/morrisjs/morris.css",
                      "~/Content/css/scrollbar/jquery.mCustomScrollbar.min.css",
                      "~/Content/css/metisMenu/metisMenu.min.css",
                      "~/Content/css/metisMenu/metisMenu-vertical.css",
                      "~/Content/css/calendar/fullcalendar.min.css",
                      "~/Content/css/calendar/fullcalendar.print.min.css",
                      "~/Content/style.css",
                      "~/Content/css/responsive.css",
                      "~/Content/datatables/css/datatables.bootstrap.css"));
        }
    }
}
