using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Services;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Web.ActionFilters;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
	public class LocationController : BaseController
	{
		private readonly LocationService _locationService;

		public LocationController()
		{
			_locationService = new LocationService();
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Add(int clubId)
		{
			var model = _locationService.GetNewLocationViewModel(clubId);

			return View("Add", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Add(LocationViewModel model)
		{
			if(ModelState.IsValid)
			{
				model = _locationService.SaveNewLocation(model);

				return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Locations", Request.Url.Authority, model.Club.ClubId));
			}

			return View("Add", model);
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Edit(int locationId)
		{
			var model = _locationService.GetEditLocationViewModel(locationId);

			return View("Edit", model);
		}

		[HttpPost, Authorize, UserIsClubAdmin]
		public ActionResult Edit(LocationViewModel model)
		{
			if (ModelState.IsValid)
			{
				model = _locationService.UpdateLocation(model);

				return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Locations", Request.Url.Authority, model.Club.ClubId));
			}

			return View("edit", model);
		}
	}
}