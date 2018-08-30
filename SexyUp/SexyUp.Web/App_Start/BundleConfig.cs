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
            bundles.Add(new StyleBundle("~/vendor/bootstrap/css").Include(
                      "~/vendor/bootstrap/css/bootstrap.min.css"));

            // font-awesome
            bundles.Add(new StyleBundle("~/vendor/font-awesome/css").Include(
                "~/vendor/font-awesome/css/font-awesome.min.css"));

            // bootstrap select
            bundles.Add(new StyleBundle("~/vendor/bootstrap-select/css").Include(
                "~/vendor/bootstrap-select/css/bootstrap-select.min.css"));

            // price slider
            bundles.Add(new StyleBundle("~/vendor/nouislider/css").Include(
                "~/vendor/nouislider/nouislider.css"));

            // custom font icons
            bundles.Add(new StyleBundle("~/content/custom-font-icons/css").Include(
                "~/Content/custom-fonticons.css"));

            // owl carousel
            bundles.Add(new StyleBundle("~/vendor/owl-carousel/css").Include(
                "~/vendor/owl.carousel/assets/owl.carousel.css",
                "~/vendor/owl.carousel/assets/owl.theme.default.css"));

            // theme
            bundles.Add(new StyleBundle("~/content/theme/css").Include(
                "~/Content/style.pink.css",
                "~/Content/custom.css"));
            #endregion

            #region JS
            // modernizr
            bundles.Add(new ScriptBundle("~/scripts/modernizr/js").Include(
                      "~/Scripts/modernizr.custom.79639.js"));

            // jquery
            bundles.Add(new ScriptBundle("~/vendor/jquery/js").Include(
                "~/vendor/jquery/jquery.min.js"));

            // popper
            bundles.Add(new ScriptBundle("~/vendor/popper/js").Include(
                "~/vendor/popper.js/umd/popper.min.js"));

            // bootstrap
            bundles.Add(new ScriptBundle("~/vendor/bootstrap/js").Include(
                "~/vendor/bootstrap/js/bootstrap.min.js"));

            // jquery cookie
            bundles.Add(new ScriptBundle("~/vendor/jquery-cookie/js").Include(
                "~/vendor/jquery.cookie/jquery.cookie.js"));

            // owl carousel
            bundles.Add(new ScriptBundle("~/vendor/owl-carousel/js").Include(
                "~/vendor/owl.carousel/owl.carousel.min.js",
                "~/vendor/owl.carousel2.thumbs/owl.carousel2.thumbs.min.js"));

            // bootstrap select
            bundles.Add(new ScriptBundle("~/vendor/bootstrap-select/js").Include(
                "~/vendor/bootstrap-select/js/bootstrap-select.min.js"));

            // noui slider
            bundles.Add(new ScriptBundle("~/vendor/nouislider/js").Include(
                "~/vendor/nouislider/nouislider.min.js"));

            // jquery-countdown
            bundles.Add(new ScriptBundle("~/vendor/jquery-countdown/js").Include(
                "~/vendor/jquery-countdown/jquery.countdown.min.js"));

            // main js
            bundles.Add(new ScriptBundle("~/scripts/main/js").Include(
                "~/scripts/front.js"));
            #endregion

        }
    }
}
