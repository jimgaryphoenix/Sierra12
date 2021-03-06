﻿using System;
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
	public class UserIsSelfAttribute : ActionFilterAttribute
	{
		private DriverService _driverService;

		public UserIsSelfAttribute()
		{
			_driverService = new DriverService();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;
			int driverId = int.Parse(filterContext.RouteData.Values["driverId"].ToString());

			string userEmailAddress = httpContext.User.Identity.Name;

			var driver = _driverService.GetDriverByEmailAddress(userEmailAddress);

			if (driver.DriverId != driverId)
			{
				filterContext.HttpContext.Response.Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Profile", 
																			httpContext.Request.Url.Authority, 
																			driver.HomeClubId.HasValue ? driver.HomeClubId.Value : 0));
			}
		}
	}
}
