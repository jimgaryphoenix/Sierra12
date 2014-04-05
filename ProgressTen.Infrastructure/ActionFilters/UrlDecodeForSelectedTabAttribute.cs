using System.Web.Mvc;

namespace ProgressTen.Infrastructure.ActionFilters
{
	/// <summary>
	/// Replaces UrlEncoding of '%23' back to '#' symbol so that it can be read to set Default Tab in Jquery Tabs group.
	/// </summary>
	public class UrlDecodeHashSymbolAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var httpContext = filterContext.HttpContext;
			var response = httpContext.Response;


			if (response.IsRequestBeingRedirected)
			{
				string location = response.RedirectLocation ?? string.Empty;

				if (!string.IsNullOrEmpty(location))
				{
					response.RedirectLocation = location.Replace("%23", "#");
				}
			}
		}

		//public override void OnResultExecuted(ResultExecutedContext filterContext)
		//{
		//    var httpContext = filterContext.HttpContext;
		//    var response = httpContext.Response;


		//    if (response.IsRequestBeingRedirected)
		//    {
		//        string location = response.RedirectLocation ?? string.Empty;

		//        if (!string.IsNullOrEmpty(location))
		//        {
		//            response.RedirectLocation = location.Replace("%23", "#");
		//        }
		//    }
		//}
	}
}