using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", 
                      "~/Scripts/respond.js"));
            
            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                     "~/Content/css/bootstrap.css",
                     "~/Content/Site.css"
                     ));

            // Стили шрифтов-кнопок и тп
            bundles.Add(new StyleBundle("~/Content/awesome").Include(
                     "~/Content/FortAwesome/css/font-awesome.min.css"
                     ));

            // Видеопроигрывание
            bundles.Add(new StyleBundle("~/YIP/css/css").Include(
                     "~/Scripts/jquery.mb.YTPlayer-master/dist/css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/YIP").Include(
                        "~/Scripts/jquery.mb.YTPlayer-master/dist/jquery.mb.YTPlayer.js"
                         ));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"
                         ));

            // Окно самописных скриптов
            bundles.Add(new ScriptBundle("~/bundles/helpdesk").Include(
                         "~/Scripts/helpdesk.js"
                         ));

            // Только для таблиц
            bundles.Add(new StyleBundle("~/Scripts/bootstrap-table-master/css").Include(
                     "~/Scripts/bootstrap-table-master/src/bootstrap-table.css",
                     "~/Scripts/bootstrap-table-master/src/bootstrap-editable.css"
                     ));            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/1").Include(
                        "~/Scripts/bootstrap-table-master/src/jquery.base64.js"
                         //"~/Scripts/bootstrap-table-master/src/extensions/reorder-columns/bootstrap-table-reorder-columns.js",
                         //"~/Scripts/bootstrap-table-master/src/extensions/reorder-rows/bootstrap-table-reorder-rows.js",                       
                         ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/2").Include(
                        "~/Scripts/bootstrap-table-master/src/bootstrap-table.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/3").Include(
                        "~/Scripts/bootstrap-table-master/src/locale/bootstrap-table-ru-RU.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/4").Include(
                         "~/Scripts/bootstrap-table-master/src/tableExport.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/5").Include(
                         "~/Scripts/bootstrap-table-master/src/bootstrap-editable.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/6").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/toolbar/bootstrap-table-toolbar.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/7").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/resizable/bootstrap-table-resizable.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/8").Include(
                          "~/Scripts/bootstrap-table-master/src/extensions/natural-sorting/bootstrap-table-natural-sorting.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/19").Include(
                          "~/Scripts/bootstrap-table-master/src/extensions/natural-sorting/bootstrap-table-multiple-sort.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/9").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/editable/bootstrap-table-editable.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/10").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/angular/bootstrap-table-angular.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/11").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/cookie/bootstrap-table-cookie.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/12").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/editable/bootstrap-table-editable.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/13").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/export/bootstrap-table-export.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/14").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/filter/bootstrap-table-filter.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/15").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/filter-control/bootstrap-table-filter-control.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/16").Include(
                         "~/Scripts/bootstrap-table-master/src/extensions/flat-json/bootstrap-table-flat-json.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/17").Include(
                          "~/Scripts/bootstrap-table-master/src/extensions/key-events/bootstrap-table-key-events.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-master/18").Include(
                          "~/Scripts/bootstrap-table-master/src/extensions/mobile/mobile.js"
                ));


        }
    }
}
