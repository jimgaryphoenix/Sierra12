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
    public class DriverController : BaseController
    {
    	private readonly DriverService _driverService;

		public DriverController()
		{
			_driverService = new DriverService();
		}

		public ActionResult Add(int? clubId, int? eventId, int? classId)
		{
			var model = new DriverViewModel();

			model.EventId = eventId;
			model.ClassId = classId;
			model.ClubId = clubId;

			model.States = _driverService.GetStateSelectList();
			model.Clubs = _driverService.GetClubSelectList(true);

			return View(model);
		}

		[HttpPost, ValidateInput(true)]
		public ActionResult Add(DriverViewModel model)
		{
			if (string.IsNullOrEmpty(model.LastName) && string.IsNullOrEmpty(model.ScreenName))
			{
				ModelState.AddModelError("ScreenName", "Please include either the RCC Screen Name or the Last Name of the driver");
				ModelState.AddModelError("LastName", "Please include either the Last Name or RCC Screen Name of the driver");
			}

			if (!string.IsNullOrEmpty(model.ScreenName) && !_driverService.CheckUniqueScreenName(model.ScreenName.ToLower()))
			{
				ModelState.AddModelError("ScreenName", "There is already a driver entered with this RCC Screen Name");
			}

			//if (!string.IsNullOrEmpty(model.EmailAddress) && !_driverService.CheckUniqueEmail(model.EmailAddress.ToLower()))
			//{
			//    ModelState.AddModelError("EmailAddress", "There is already a driver entered with this email address");
			//}

			if (ModelState.IsValid)
			{
				_driverService.RegisterNewDriver(model);

				var returnUrl = "http://" + Request.Url.Authority;

				if(model.EventId > 0 && model.ClassId > 0)
				{
					returnUrl += "/Score/Add/Event/" + model.EventId + "/Club/" + model.ClubId + "/Class/" + model.ClassId;
				}
				else
				{
					returnUrl += "/Club/" + model.ClubId + "?selectedTab=#Drivers";
				}

				return Redirect(returnUrl);
			}

			model.States = _driverService.GetStateSelectList();
			model.Clubs = _driverService.GetClubSelectList(true);

			return View(model);
		}

		[Authorize, UserIsClubAdmin]//, UserIsSelf]
		public ActionResult Edit(int driverId)
		{
			var model = _driverService.GetEditDriverViewModel(driverId);

			return View(model);
		}

		[HttpPost, Authorize, UrlDecodeHashSymbol, UserIsClubAdmin]//, UserIsSelf]
		public ActionResult Edit(DriverViewModel model)
		{
			if(ModelState.IsValid)
			{
				_driverService.UpdateDriver(model);

				int clubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);

				return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Profile", Request.Url.Authority, clubId));
			}

			model.States = _driverService.GetStateSelectList();
			model.Clubs = _driverService.GetClubSelectList(true);

			return View(model);
		}

		[Authorize, UserIsSelf]
		public ActionResult ChangePassword(int driverId)
		{
			var model = _driverService.GetChangePasswordViewModel(driverId);

			int clubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);

			model.ClubId = clubId;

			return View(model);
		}

		[HttpPost, Authorize, UrlDecodeHashSymbol, UserIsSelf]
		public ActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (!string.IsNullOrEmpty(model.NewPassword) &&
				!string.IsNullOrEmpty(model.ConfirmNewPassword) &&
				!model.NewPassword.Equals(model.ConfirmNewPassword))
			{
				ModelState.AddModelError("NewPassword", "New Password and New Password Confirmation do not match");
			}

			if(!_driverService.VerifyOldPassword(model))
			{
				ModelState.AddModelError("OldPassword", "Old password does not match our records");
			}

			if (ModelState.IsValid)
			{
				if(!_driverService.ChangePassword(model))
				{
					ModelState.AddModelError("ErrorMessage", "There was a problem processing this change. If you cannot login with either your new or old password, please contact us via our contact page.");
					return View(model);
				}

				//int clubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);
				model.StatusMessage = MvcHtmlString.Create("Your password as been changed. Use your new password the next time you login");

				//return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Profile", Request.Url.Authority, model.ClubId));
				return View("ChangePasswordSuccess", model);
			}

			return View(model);
		}

		//[Authorize, UrlDecodeHashSymbol, UserIsClubAdmin]
		//public ActionResult Approve(int driverId, int clubId)
		//{
		//    _driverService.SetDriverApproved(driverId);

		//    int adminUserClubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);

		//    return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Drivers", Request.Url.Authority, adminUserClubId));
		//}

		//[Authorize, UrlDecodeHashSymbol, UserIsClubAdmin]
		//public ActionResult Decline(int driverId)
		//{
		//    _driverService.SetDriverDeclined(driverId);

		//    int adminUserClubId = _driverService.GetHomeClubIdForDriver(User.Identity.Name);

		//    return Redirect(string.Format("http://{0}/Club/{1}?selectedTab=#Drivers", Request.Url.Authority, adminUserClubId));
		//}
    }
}
