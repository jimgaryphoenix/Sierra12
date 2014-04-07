using System;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;
using ProgressTen.Web.ActionFilters;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
    public class EventController : BaseController
    {
    	private readonly EventService _eventService;

		public EventController()
		{
			_eventService = new EventService();
		}

		//[CheckAuthenticationAttribute(true)]
		//public ActionResult Index(int? eventId)
		//{
		//    var comp = _eventService.GetEventInfoById(eventId);

		//    return View(comp);
		//}

		[Authorize, UserIsClubAdmin]
		public ActionResult Add(int clubId, int? seriesId)
		{
			var model = _eventService.GetNewEventViewModel(clubId, seriesId);

			return View("Add", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Add(EventViewModel model)
		{
			if (ModelState.IsValid)
			{
				model = _eventService.SaveNewEvent(model);

				return Redirect(string.Format("http://{0}/Club/{1}/Series/{2}", Request.Url.Authority, model.ClubId, model.SeriesId));
			}

			model.Locations = _eventService.GetLocationSelectList(model.ClubId);

			return View("Add", model);
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Edit(int eventId, int clubId)
		{
			var model = _eventService.GetEditEventViewModel(eventId, clubId);

			return View("Edit", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Edit(EventViewModel model)
		{
			if (ModelState.IsValid)
			{
				model = _eventService.UpdateEvent(model);

				return Redirect(string.Format("http://{0}/Club/{1}", Request.Url.Authority, model.ClubId));
			}

			model.Locations = _eventService.GetLocationSelectList(model.ClubId);

			return View("Edit", model);
		}
    }
}
