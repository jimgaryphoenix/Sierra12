using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.ActionFilters
{
	public class UserIsClubAdminAttribute : ActionFilterAttribute
	{
		private DriverService _driverService;

		public UserIsClubAdminAttribute()
		{
			_driverService = new DriverService();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;
			int clubId = int.Parse(filterContext.RouteData.Values["clubId"].ToString());

			string userEmailAddress = httpContext.User.Identity.Name;

			var driver = _driverService.GetDriverByEmailAddress(userEmailAddress);

			string roleName = "ClubAdmin_" + clubId;

			if (!Roles.IsUserInRole(userEmailAddress, roleName) && !Roles.IsUserInRole(userEmailAddress, "SiteAdmin"))
			{
				filterContext.HttpContext.Response.Redirect(string.Format("http://{0}/Club/{1}", httpContext.Request.Url.Authority, driver.HomeClubId));
			}
		}
	}
}
