using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Infrastructure.ActionFilters
{
	public class CheckAuthenticationAttribute : ActionFilterAttribute
	{
		private readonly bool _causesRedirect;

		public CheckAuthenticationAttribute(bool causesRedirect)
		{
			_causesRedirect = causesRedirect;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;

			//This was mostly just a dump for Copperstate to send any non-authenticated user back, on the assumption that, at the time, ALL authenticated users were admins
			if (_causesRedirect && !httpContext.User.Identity.IsAuthenticated && httpContext.Request.Url.AbsolutePath.ToLower().Contains("admin"))
			{
				filterContext.HttpContext.Response.Redirect("~/Login");
			}
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var viewModelBase = filterContext.Controller.ViewData.Model as ViewModelBase;

			viewModelBase.IsAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;
		}
	}
}
