using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;
using ProgressTen.Web.ActionFilters;

namespace ProgressTen.Web.Controllers
{
    public class ClubController : BaseController
    {
    	private readonly ClubService _clubService;

		public ClubController()
		{
			_clubService = new ClubService();
		}

		//[Authorize, SetLoggedInDriver]
		public ActionResult Index(int clubId, int? selectedSeriesId, int? selectedEventId)
		{
			var model = _clubService.GetClubDisplayViewModel(clubId, User.Identity, selectedSeriesId, selectedEventId);

		    return View(model);
		}

		public ActionResult Register()
		{
			var model = new RegisterClubViewModel();
			model.States = _clubService.GetStateSelectList();
			model.Regions = _clubService.GetRegionSelectList();
			model.Countries = _clubService.GetCountrySelectList();

			return View(model);
		}

		[HttpPost, ValidateInput(true)]
		public ActionResult Register(RegisterClubViewModel newClubModel)
		{
			if (!string.IsNullOrEmpty(newClubModel.Password) && 
				!string.IsNullOrEmpty(newClubModel.ConfirmPassword) && 
				!newClubModel.Password.Equals(newClubModel.ConfirmPassword))
			{
				ModelState.AddModelError("Password", "Password and Password Confirmation do not match");
			}

			if(!string.IsNullOrEmpty(newClubModel.EmailAddress) && !_clubService.CheckUniqueEmail(newClubModel.EmailAddress.ToLower()))
			{
				ModelState.AddModelError("EmailAddress", "There is already a registered user with this email address");
			}

			if (!ModelState.IsValid)
			{
				newClubModel.States = _clubService.GetStateSelectList();
				newClubModel.Regions = _clubService.GetRegionSelectList();
				newClubModel.Countries = _clubService.GetCountrySelectList();

				return View("Register", newClubModel);
			}

			_clubService.RegisterNewClub(newClubModel);

			return View("RegisterSuccess", newClubModel);
		}

		//[Authorize, UserIsClubAdmin]//, UserIsSelf]
		//public ActionResult Edit(int clubId)
		//{
		//    var model = _clubService.GetClubViewModel(clubId);

		//    return View(model);
		//}

		//[HttpPost, Authorize, UrlDecodeHashSymbol, UserIsClubAdmin]//, UserIsSelf]
		//public ActionResult Edit(DriverViewModel model)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        _driverService.UpdateDriver(model);

		//        int clubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);

		//        return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Profile", Request.Url.Authority, clubId));
		//    }

		//    model.States = _driverService.GetStateSelectList();
		//    model.Clubs = _driverService.GetClubSelectList(true);

		//    return View(model);
		//}

		[Authorize, UrlDecodeHashSymbol, UserIsSiteAdmin]
		public ActionResult Approve(int clubId)
		{
			_clubService.SetClubApproved(clubId);

			int adminUserClubId = _clubService.GetHomeClubIdForDriver(User.Identity.Name);

			return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Clubs", Request.Url.Authority, adminUserClubId));
		}

		[Authorize, UrlDecodeHashSymbol, UserIsSiteAdmin]
		public ActionResult Decline(int clubId)
		{
			_clubService.SetClubDeclined(clubId);

			int adminUserClubId = _clubService.GetHomeClubIdForDriver(User.Identity.Name);

			return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Clubs", Request.Url.Authority, adminUserClubId));
		}
    }
}
