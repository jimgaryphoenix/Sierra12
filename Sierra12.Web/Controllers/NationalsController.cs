using System.Web.Mvc;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
	public class NationalsController : BaseController
	{
		//[CheckAuthenticationAttribute(true)]
		//public ActionResult Index()
		//{
		//    var service = new EventService();
		//    EventListViewModel eventsView = service.GetUpcomingNationalEvents();

		//    return View("Index", eventsView);
		//}
	}
}
