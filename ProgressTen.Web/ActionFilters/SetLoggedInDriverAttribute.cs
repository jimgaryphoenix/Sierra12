using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.ActionFilters
{
	public class SetLoggedInDriverAttribute : ActionFilterAttribute
	{
		private DriverService _driverService;

		public SetLoggedInDriverAttribute()
		{
			_driverService = new DriverService();
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;
			var viewModelBase = filterContext.Controller.ViewData.Model as ViewModelBase;

			var userEmailAddress = httpContext.User.Identity.Name;

			var loggedInDriver = _driverService.GetDriverByEmailAddress(userEmailAddress);

			if(loggedInDriver != null)
			{
				viewModelBase.LoggedInDriver = loggedInDriver;
			}
			else
			{
				FormsAuthentication.SignOut();

				httpContext.Response.Redirect(string.Format("http://{0}/Login", httpContext.Request.Url.Authority));
			}
		}
	}
}
