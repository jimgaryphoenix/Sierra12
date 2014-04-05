using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
    public class EventsController : BaseController
    {
    	private readonly EventService _eventService;

		public EventsController()
		{
			_eventService = new EventService();
		}

		[CheckAuthentication(true)]
        public ActionResult Index(int? eventId)
        {
			var comp = _eventService.GetEventInfoById(eventId);

            return View(comp);
        }
    }
}
