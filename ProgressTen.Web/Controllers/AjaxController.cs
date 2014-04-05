using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
    public class AjaxController : BaseController
    {
		private readonly DriverService _driverService;
		private readonly ClubService _clubService;

		public AjaxController()
		{
			_driverService = new DriverService();
			_clubService = new ClubService();
		}

		[HttpPost, Authorize]
		public ActionResult GetDriverSuggest(string searchText, int maxResults, int clubId, bool clubOnly)
		{
			var drivers = _driverService.GetDriverAutoComplete(searchText, maxResults, clubId, clubOnly);

			return Json(drivers);
		}

		[HttpPost]
		public ActionResult GetClubSuggest(string searchText)
		{
			var clubs = _clubService.GetClubAutoComplete(searchText);

			return Json(clubs);
		}
    }
}
