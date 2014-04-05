using System;
using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Services;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Web.ActionFilters;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
	public class SeriesController : BaseController
	{
		private readonly SeriesService _seriesService;
		private readonly ClubService _clubService;
		private readonly EventService _eventService;

		public SeriesController()
		{
			_seriesService = new SeriesService();
			_clubService = new ClubService();
			_eventService = new EventService();
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Add(int clubId)
		{
			var model = _seriesService.GetNewSeriesViewModel(clubId);

			model.BeginDate = DateTime.Now;

			return View("Add", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Add(SeriesViewModel model)
		{
			if (ModelState.IsValid)
			{
				model = _seriesService.SaveNewSeries(model);

				return Redirect(string.Format("http://{0}/Club/{1}/Series/{2}", Request.Url.Authority, model.Club.ClubId, model.SeriesId));
			}

			return View("Add", model);
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Edit(int seriesId)
		{
			var model = _seriesService.GetEditSeriesViewModel(seriesId);

			return View("Edit", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Edit(SeriesViewModel model)
		{
			if (ModelState.IsValid)
			{
				model = _seriesService.UpdateSeries(model);

				return Redirect(string.Format("http://{0}/Club/{1}", Request.Url.Authority, model.Club.ClubId));
			}

			return View("Edit", model);
		}
	}
}