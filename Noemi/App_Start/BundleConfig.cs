using System.Web;
using System.Web.Optimization;

namespace Noemi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-hover-dropdown.min.js",
                "~/Scripts/waypoints.min.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/isotope.pkgd.min.js",
                "~/Scripts/jquery.magnific-popup.min.js",
                "~/Scripts/creative-brands.js",
                "~/Scripts/jqBootstrapValidation.js",
                "~/Scripts/jquery.mb.YTPlayer.min.js",
                "~/Scripts/jquery.counterup.min.js",
                "~/Scripts/jquery.countdown.min.js",
                "~/Scripts/custom.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/animate.css",
                "~/Content/jquery.bootstrap-touchspin.css",
                "~/Content/owl.carousel.css",
                "~/Content/magnific-popup.css",
                "~/Content/creative-brands.css",
                "~/Content/custom.css",
                "~/Content/Site.css"
                ));
        }
    }
}
