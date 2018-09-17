using System.Web.Optimization;

namespace SexyUp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.validate.js",
                                "~/Scripts/jquery.validate.unobtrusive.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css"));

            #region CSS

            // bootstrap
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/vendor/bootstrap/css/bootstrap.min.css"));

            // font-awesome
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                "~/vendor/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // bootstrap select
            bundles.Add(new StyleBundle("~/bundles/bootstrap-select/css").Include(
                "~/vendor/bootstrap-select/css/bootstrap-select.min.css"));

            // price slider
            bundles.Add(new StyleBundle("~/bundles/nouislider/css").Include(
                "~/vendor/nouislider/nouislider.css"));

            // custom font icons
            bundles.Add(new StyleBundle("~/bundles/custom-font-icons/css").Include(
                "~/Content/custom-fonticons.css", new CssRewriteUrlTransform()));

            // owl carousel
            bundles.Add(new StyleBundle("~/bundles/owl-carousel/css").Include(
                "~/vendor/owl.carousel/assets/owl.carousel.css",
                "~/vendor/owl.carousel/assets/owl.theme.default.css"));

            // theme
            bundles.Add(new StyleBundle("~/bundles/theme/css").Include(
                "~/Content/style.pink.css",
                "~/Content/custom.css"));

            // toastr
            bundles.Add(new StyleBundle("~/bundles/toastr/css").Include(
                "~/vendor/toastr/css/toastr.min.css"));

            // datatables
            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                "~/vendor/datatables/css/dataTables.bootstrap4.min.css"));

            // datepicker
            bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker/css").Include(
                "~/vendor/bootstrap-datepicker/css/bootstrap-datepicker.standalone.min.css"));
            #endregion

            #region JS
            // modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr/js").Include(
                      "~/Scripts/modernizr.custom.79639.js"));

            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                "~/vendor/jquery/jquery.min.js"));

            // popper
            bundles.Add(new ScriptBundle("~/bundles/popper/js").Include(
                "~/vendor/popper.js/umd/popper.min.js"));

            // bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                "~/vendor/bootstrap/js/bootstrap.min.js"));

            // jquery cookie
            bundles.Add(new ScriptBundle("~/bundles/jquery-cookie/js").Include(
                "~/vendor/jquery.cookie/jquery.cookie.js"));

            // owl carousel
            bundles.Add(new ScriptBundle("~/bundles/owl-carousel/js").Include(
                "~/vendor/owl.carousel/owl.carousel.min.js",
                "~/vendor/owl.carousel2.thumbs/owl.carousel2.thumbs.min.js"));

            // bootstrap select
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select/js").Include(
                "~/vendor/bootstrap-select/js/bootstrap-select.min.js",
                "~/vendor/bootstrap-select/js/i18n/defaults-pt_BR.min.js"));

            // noui slider
            bundles.Add(new ScriptBundle("~/bundles/nouislider/js").Include(
                "~/vendor/nouislider/nouislider.min.js"));

            // jquery-countdown
            bundles.Add(new ScriptBundle("~/bundles/jquery-countdown/js").Include(
                "~/vendor/jquery-countdown/jquery.countdown.min.js"));

            // main js
            bundles.Add(new ScriptBundle("~/bundles/main/js").Include(
                "~/scripts/front.js"));

            // toastr
            bundles.Add(new ScriptBundle("~/bundles/toastr/js").Include(
                "~/vendor/toastr/js/toastr.min.js"));

            // datatables
            bundles.Add(new ScriptBundle("~/bundles/datatables/js").Include(
                "~/vendor/datatables/js/jquery.dataTables.min.js",
                "~/vendor/datatables/js/dataTables.bootstrap4.min.js",
                "~/vendor/datatables/js/datatable-config.js"));

            // jquery mask
            bundles.Add(new ScriptBundle("~/bundles/jquery-mask/js").Include(
                "~/vendor/jquery-mask/js/jquery.mask.min.js",
                "~/vendor/jquery-mask/js/jquery.mask-config.js"));

            // jquery mask
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker/js").Include(
                "~/vendor/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                "~/vendor/bootstrap-datepicker/locales/bootstrap-datepicker.pt-BR.min.js",
                "~/vendor/bootstrap-datepicker/js/bootstrap-datepicker-config.js"));


            #endregion

        }
    }
}
