using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgressTen.Web.Controllers
{
    public class BaseController : Controller
    {
    	protected override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
		{
			base.OnActionExecuted(actionExecutedContext);

			ViewBag.UrlAuthority = Request.Url.Authority;
		}
    }
}
