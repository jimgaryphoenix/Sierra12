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
	public class UserIsSiteAdminAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;

			if (!Roles.IsUserInRole(httpContext.User.Identity.Name, "SiteAdmin"))
			{
				FormsAuthentication.SignOut();

				filterContext.HttpContext.Response.Redirect(string.Format("http://{0}/Login", httpContext.Request.Url.Authority));
			}
		}
	}
}
