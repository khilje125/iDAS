using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace iDAS
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                //"~/vendors/jquery/dist/jquery.min.js",
                "~/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/vendors/fastclick/lib/fastclick.js",
                "~/vendors/nprogress/nprogress.js",
                "~/vendors/Chart.js/dist/Chart.min.js",
                "~/vendors/bernii/gauge.js/dist/gauge.min.js",
                "~/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/vendors/iCheck/icheck.min.js",
                "~/vendors/skycons/skycons.js",
                "~/vendors/Flot/jquery.flot.js",
                "~/vendors/Flot/jquery.flot.pie.js",
                "~/vendors/Flot/jquery.flot.time.js",
                "~/vendors/Flot/jquery.flot.stack.js",
                "~/vendors/Flot/jquery.flot.resize.js",
                "~/Scripts/flot/jquery.flot.orderBars.js",
                "~/Scripts/flot/date.js",
                "~/Scripts/flot/jquery.flot.spline.js",
                "~/Scripts/flot/curvedLines.js",
                "~/Scripts/maps/jquery-jvectormap-2.0.3.min.js",
                "~/Scripts/moment/moment.min.js",
                "~/Scripts/datepicker/daterangepicker.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                 "~/vendors/validator/validator.min.js",
                "~/Scripts/iPaymentService.min.js"
                ));
            bundles.Add(new ScriptBundle("~/js/DataTable").Include(
                "~/vendors/datatables.net/js/jquery.dataTables.min.js",
                "~/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                "~/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                "~/vendors/datatables.net-buttons/js/buttons.print.min.js",
                "~/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                "~/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                "~/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                "~/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                "~/vendors/datatables.net-scroller/js/datatables.scroller.min.js",
                "~/vendors/jszip/dist/jszip.min.js",
                "~/vendors/pdfmake/build/pdfmake.min.js",
                "~/Scripts/pdfmake/build/vfs_fonts.js",
                 "~/Scripts/DataTableInitialise.js"
                ));

            bundles.Add(new StyleBundle("~/contents/css").Include(
                "~/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/vendors/font-awesome/css/font-awesome.min.css",
                "~/vendors/iCheck/skins/flat/green.css",
                "~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                "~/Content/css/iPaymentService.min.css"
                ));
        }
    }
}