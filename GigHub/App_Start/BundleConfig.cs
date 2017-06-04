using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/scripts/app/bootbox/dialogs.js",
                        "~/scripts/app/services/attendanceService.js",//should before controller. look for "requirejs" for better solution
                        "~/scripts/app/controllers/gigsController.js",
                        "~/scripts/app/services/followingService.js",
                        "~/scripts/app/controllers/followingsController.js",
                        "~/scripts/app/services/notificationService.js",
                        "~/scripts/app/controllers/notificationController.js",
                        "~/scripts/app/services/gigService.js",
                        "~/scripts/app/controllers/gigActionsController.js",

                        "~/scripts/app/services/accountsService.js",
                        "~/scripts/app/controllers/accountStatusController.js",
                        "~/scripts/app/controllers/accountsTableController.js",
                        "~/scripts/app/controllers/changeAccountRoleController.js",
                        "~/scripts/app/services/accountRolesService.js",
                        "~/scripts/app/controllers/accountRolesController.js",
                        "~/scripts/app/controllers/accountDescriptionsController.js",
                        "~/scripts/app/controllers/readMoreOrLessController.js",
                        "~/scripts/app/services/artistsService.js",
                        "~/scripts/app/controllers/moreArtistsController.js",
                        "~/scripts/app/controllers/moreGigsController.js",
                        "~/scripts/app/controllers/moreFollowingsController.js",
                        "~/scripts/app/controllers/searchController.js",
                        "~/scripts/app/typeahead/typeaheadConfig.js",
                        "~/scripts/app/app.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(

                         "~/Scripts/respond.js",
                         "~/Scripts/bootbox.min.js",
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/underscore-min.js",
                         "~/Scripts/dataTables/jquery.datatables.js",
                         "~/Scripts/dataTables/datatables.bootstrap.js",
                         "~/Scripts/moment.js",
                         "~/Scripts/bootstrap-datetimepicker.js",
                         "~/Scripts/typeahead.bundle.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.min.css",
                "~/Content/typeahead.css",
                "~/Content/Animate.css"));

            bundles.Add(new StyleBundle("~/Content/web/css").Include(
                      "~/Content/bootstrap-theme-custom.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/datatables/css/datatables.bootstrap.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/landing/lib").Include(
                "~/Scripts/landing/jquery.js",
                "~/Scripts/landing/bootstrap.js",
                "~/Scripts/landing/jQuery-easing.js",
                "~/Scripts/landing/landing.js"
            ));



            bundles.Add(new StyleBundle("~/Content/landing/css").Include(
                "~/Content/bootstrap-theme-custom.css",
                "~/Content/landing/style.css"));
        }
    }
}
