using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Web.HtmlHelpers
{
    public static class DriverGreetingHelper
    {
        public static MvcHtmlString DriverGreeting(this HtmlHelper helper, DisplayClubViewModel model)
        {
			//RouteData route = helper.ViewContext.RequestContext.RouteData;
			//string controller = route.GetRequiredString("controller");
			//string action = route.GetRequiredString("action");
            string greeting = "\n\n<h3>";

			//foreach (MenuTab menuTab in tabs)
			//{
			//    if (controller == menuTab.Controller && action == menuTab.Action)
			//    {
			//        greeting += "\n\t<li class='selected'>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
			//    }
			//    else
			//    {
			//        greeting += "\n\t<li>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
			//    }
			//}

			if(model.Club == null)
			{
				greeting += string.Format("No Affiliated Club, {0}", model.LoggedInDriver.FullName);
			}
			else if (model.LoggedInDriver.HomeClubId != model.Club.ClubId)
			{
				greeting += string.Format("Viewing as Guest of {0} ({1}), {2}", model.Club.FullName, model.Club.Acronym, model.LoggedInDriver.FullName);
			}
			else
			{
				greeting += string.Format("{0} ({1}), {2}", model.Club.FullName, model.Club.Acronym, model.LoggedInDriver.FullName);
			}

            greeting += "\n</h3>\n\n";

            return MvcHtmlString.Create(greeting);
        }

		//public static MvcHtmlString DriverMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs)
		//{
		//    RouteData route = helper.ViewContext.RequestContext.RouteData;
		//    string controller = route.GetRequiredString("controller");
		//    string action = route.GetRequiredString("action");
		//    string menu = "\n\n<ul id='driverMenu' class='mainnav'>";

		//    foreach (MenuTab menuTab in tabs)
		//    {
		//        if (controller == menuTab.Controller && action == menuTab.Action)
		//        {
		//            menu += "\n\t<li class='selected'>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
		//        }
		//        else
		//        {
		//            menu += "\n\t<li>" + helper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller, new { area = menuTab.Area }, null) + "</li>";
		//        }
		//    }

		//    menu += "\n</ul>\n\n";

		//    return MvcHtmlString.Create(menu);
		//}
    }
}