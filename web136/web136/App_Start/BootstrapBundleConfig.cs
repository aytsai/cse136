using Web136;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(BootstrapBundleConfig), "RegisterBundles")]

namespace Web136
{
    using System.Web.Optimization;

    public class BootstrapBundleConfig
    {
        public static void RegisterBundles()
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css"));
        }
    }
}
