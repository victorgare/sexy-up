using System.Web.Optimization;

namespace SexyUp.Web
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            #region CSS
            // Font Awesome icons style
            bundles.Add(new StyleBundle("~/Vendor/fontawesome/css").Include(
                "~/Vendor/fontawesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Vendor/bootstrap/css").Include(
                "~/Vendor/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Custom").Include(
                "~/Content/site.css"));
            #endregion

            #region JS
            bundles.Add(new ScriptBundle("~/Vendor/bootstrap/js").Include(
                "~/Vendor/bootstrap/js/jquery-3.3.1.slim.min.js",
                "~/Vendor/bootstrap/js/popper.min.js",
                "~/Vendor/bootstrap/js/bootstrap.min.js"));
            #endregion

        }
    }
}
