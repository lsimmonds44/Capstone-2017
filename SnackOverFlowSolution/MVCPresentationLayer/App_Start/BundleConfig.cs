using System.Web;
using System.Web.Optimization;

namespace MVCPresentationLayer
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Configures all of the bundles for jquery
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
       /// <summary>
       /// Skyler Hiscock
       /// 
       /// Created:
       /// 2017/04/19
       /// </summary>
       /// 
       /// <remarks>
       /// Ariel Sigo
       /// Updated:
       /// 2017/04/29
       /// 
       /// Standardized Comment
       /// </remarks>
       /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

 
            bundles.Add(new ScriptBundle("~/bundles/searchproducts").Include("~/Scripts/searchproducts.js"));

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
        }
    }
}
