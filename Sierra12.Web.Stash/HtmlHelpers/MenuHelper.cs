using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Web.HtmlHelpers
{
    public static class MenuHelper
    {
        #region Public Methods

        public static string TabbedMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs)
        {
            RouteData route = helper.ViewContext.RequestContext.RouteData;
            string controller = route.GetRequiredString("controller");
            string action = route.GetRequiredString("action");
            string menu = "\n\n<ul id='menu' class='mainnav'>";

            foreach (MenuTab menuTab in tabs)
            {
                if (controller == menuTab.Controller && action == menuTab.Action)
                {
                    menu += "\n\t<li class='selected'>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
                }
                else
                {
                    menu += "\n\t<li>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
                }
            }

            menu += "\n</ul>\n\n";

            return menu;
        }

        #endregion
    }
}