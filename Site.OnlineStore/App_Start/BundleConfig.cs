﻿using System.Web;
using System.Web.Optimization;

namespace Site.OnlineStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminScript").Include(
                        "~/Scripts/admin/common.js",
                        "~/Content/admin/chosen-library/chosen.jquery.min.js",
                        "~/Content/admin/plugins/spin/spin.min.js",
                        "~/Content/admin/plugins/daterangepicker/moment.js",
                        "~/Content/admin/plugins/daterangepicker/daterangepicker.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/others").Include(
                      "~/Content/admin/dist/js/app.min.js",
                      "~/Content/admin/respond/1.4.2/respond.min.js",
                      "~/Content/admin/html5shiv/3.7.2/html5shiv.min.js"));

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                      "~/Content/bootstrap.css",  
                      "~/Content/admin/font-awesome/4.3.0/css/font-awesome.min.css",
                      "~/Content/admin/ionicons/2.0.1/css/ionicons.min.css",
                      "~/Content/admin/dist/css/AdminLTE.css",
                      "~/Content/admin/dist/css/AdminLTE.addon.css",
                      "~/Content/admin/dist/css/skins/skin-green.min.css",
                      "~/Content/common.css",
                      "~/Content/admin/chosen-library/chosen.min.css",
                      "~/Content/admin/plugins/daterangepicker/daterangepicker-bs3.css"
                      ));
        }
    }
}
