using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using log4net;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class DriverService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DriverService).Name);

		private readonly DriverRepository _driverRepository;
		private readonly ClubService _clubService;

		public DriverService()
		{
			_driverRepository = new DriverRepository();

			_clubService = new ClubService();
		}

		public Driver GetDriverByEmailAddress(string emailAddress)
		{
			return _driverRepository.GetDriverByEmailAddress(emailAddress);
		}

		public DriverViewModel RegisterNewDriver(DriverViewModel driverModel)
		{
			var newDriver = new Driver
			                	{
									FirstName = driverModel.FirstName, 
									LastName = driverModel.LastName ?? string.Empty, 
									RccScreenName = driverModel.ScreenName ?? string.Empty, 
									City = driverModel.City ?? string.Empty, 
									State = !driverModel.State.Equals("-1") ? driverModel.State : string.Empty, 
									EmailAddress = driverModel.EmailAddress ?? string.Empty,
									DateActivated = DateTime.Today, 
									DateCreated = DateTime.Now
								};

			if (driverModel.HomeClubId > 0)
			{
				newDriver.HomeClubId = driverModel.HomeClubId;
			}

			try
			{
				//Membership.CreateUser(newDriver.EmailAddress, driverModel.Password, newDriver.EmailAddress);

				_driverRepository.SaveNewDriver(newDriver);

//                if(driverModel.HomeClubId == -2)
//                {
//                    driverModel.StatusMessage = MvcHtmlString.Create("<p>" +
//                                                                        "Thank you for registering as Driver on ProgressTen.com. Since your club is not yet a ProgressTen " +
//                                                                        "subscribing club, you will be able to view other clubs pages as a guest and are able to be included " + 
//                                                                        "in events with any ProgressTen club that you may visit." +
//                                                                        "</p>" + 
//                                                                        "<p>" + 
//                                                                        "Once your club is subscribed with ProgressTen.com, you will be able to connect yourself to that club " + 
//                                                                        "by editing your profile and selecting your club.");
//                }
//                else
//                {
				driverModel.StatusMessage = MvcHtmlString.Create("<p>" +
																	"The new driver, " + newDriver.FullDisplayName + ", has been added.</p>" +
																	"<p>Return To Scores Page</p>" +
																	"<p>Return To Dirvers List</p>");

//                    using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
//                    {
//                        var message = new MailMessage();

//                        var homeClub = _clubService.GetClubById(newDriver.HomeClubId.GetValueOrDefault(0));

//                        string toAddress = homeClub.CurrentPresident.EmailAddress;

//                        message.To.Add(new MailAddress(toAddress));
//                        message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen Registration");
//                        message.Subject = "ProgressTen - New Driver Registration";
//                        message.Body = @"
//A new driver has registered with ProgressTen.com and has selected " + homeClub.Acronym + @" as thier home club:\n\n
//Driver Name: " + newDriver.FullName + @"\n
//ScreenName: " + newDriver.RccScreenName + @"\n
//City: " + newDriver.City + @"\n
//State: " + newDriver.State + @"\n
//Email: " + newDriver.EmailAddress + @"\n
//
//Yourself or any designated ProgressTen.com club admin can approve or decline this new driver on the Drivers tab in your club's area.
//
//Thanks
//ProgressTen.com";

//                        _mailClient.Send(message);
//                    }
//                }
			}
			catch (Exception ex)
			{
				log.Error("Problem Creating New Driver", ex);

				driverModel.StatusMessage = MvcHtmlString.Create("<p>" + 
																	"There was a problem submitting this new driver. You may double check that all your data was " +
																	"entered correctly and try again. ProgressTen.com administrators have been made aware of the " +
																	"error and will look into the matter further." + 
																	"</p>");
			}

			return driverModel;
		}

		public IList<Driver> GetDriverAutoComplete(string searchText, int maxResults, int clubId, bool clubOnly)
		{
			var drivers = _driverRepository.GetDriverAutoComplete(searchText, maxResults, clubId, clubOnly);

			return drivers.Select(driver => new Driver {FullDisplayName = driver.FullDisplayName, DriverId = driver.DriverId}).ToList();
		}

		public DriverViewModel GetEditDriverViewModel(int driverId)
		{
			var model = new DriverViewModel();

			var driver = _driverRepository.GetDriverById(driverId);

			model.DriverId = driverId;
			model.FirstName = driver.FirstName;
			model.LastName = driver.LastName;
			model.EmailAddress = driver.EmailAddress;
			model.ScreenName = driver.RccScreenName;
			model.HomeClubId = driver.HomeClubId.GetValueOrDefault(0);
			model.City = driver.City;
			model.State = driver.State;

			//model.Password = "DummyValue";
			//model.ConfirmPassword = "DummyValue";

			model.States = GetStateSelectList();
			model.Clubs = GetClubSelectList(true);

			return model;
		}

		public DriverViewModel UpdateDriver(DriverViewModel model)
		{
			var driver = _driverRepository.GetDriverById(model.DriverId);

			Club oldClub = null;
			Club newClub = null;

			driver.FirstName = model.FirstName;
			driver.LastName = model.LastName ?? string.Empty;
			driver.RccScreenName = model.ScreenName ?? string.Empty;
			driver.City = model.City ?? string.Empty;
			driver.State = model.State.Equals("-1") ? model.State : string.Empty;
			driver.EmailAddress = model.EmailAddress ?? string.Empty;

			if(model.ChangeHomeClubId)
			{
				oldClub = driver.HomeClub;

				if(model.HomeClubId > 0)
				{
					newClub = _clubService.GetClubById(model.HomeClubId);
				}

				driver.HomeClubId = newClub.ClubId;
				driver.DateActivated = DateTime.Today;

//                using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
//                {
//                    var message = new MailMessage();

//                    message.To.Add(new MailAddress(oldClub.CurrentPresident.EmailAddress));

//                    string additionForNewClub = string.Empty;
//                    string newClubName = "no affilliation";

//                    if(newClub != null)
//                    {
//                        message.To.Add(new MailAddress(newClub.CurrentPresident.EmailAddress));

//                        additionForNewClub = @"As the admin of the new club, you will find the new driver's request under the Drivers tab on your club's page.";
//                        newClubName = newClub.Acronym;
//                    }

//                    message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen.com");
//                    message.Subject = "ProgressTen - Club Affiliation Change";
//                    message.Body = @"
//The driver " + driver.FullDisplayName + @", has updated his profile on ProgressTen.com to change clubs from " + oldClub.Acronym + @" to " + newClubName + @". As the 
//admin of the club that " + driver.FirstName + @" is leaving, we just wanated to inform you that " + driver.FirstName + @" is no longer listed as an active member of your club. 
//
//" + additionForNewClub + @"
//
//Thank you
//ProgressTen.com";

//                    _mailClient.Send(message);
//                }
			}

			_driverRepository.UpdateDriver(driver);

			return model;
		}

		public ChangePasswordViewModel GetChangePasswordViewModel(int driverId)
		{
			return new ChangePasswordViewModel{DriverId = driverId};
		}

		public bool ChangePassword(ChangePasswordViewModel model)
		{
			var driver = _driverRepository.GetDriverById(model.DriverId);

			var user = Membership.GetUser(driver.EmailAddress);

			if (user != null)
			{
				return user.ChangePassword(model.OldPassword, model.NewPassword);
			}

			return false;
		}

		public bool VerifyOldPassword(ChangePasswordViewModel model)
		{
			var driver = _driverRepository.GetDriverById(model.DriverId);
			bool authenticated = Membership.ValidateUser(driver.EmailAddress, model.OldPassword);

			return authenticated;
		}

		//        public void SetDriverApproved(int driverId)
		//        {
		//            try
		//            {
		//                var driver = _driverRepository.GetDriverById(driverId);

		//                driver.DateActivated = DateTime.Now;
		//                driver.DateCancelled = null;

		//                _driverRepository.UpdateDriver(driver);

		//                using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
		//                {
		//                    var message = new MailMessage();

		//                    string toAddress = driver.EmailAddress;

		//                    message.To.Add(new MailAddress(toAddress));
		//                    message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen.com Registration");
		//                    message.Subject = "ProgressTen - Club Affiliation";
		//                    message.Body = @"
		//You have been verified as an active member of " + driver.HomeClub.Acronym + @" by the admin for your club. When 
		//you next login at ProgressTen.com, you will see your club's schedule, results, and other club features.
		//
		//Thank you
		//ProgressTen.com";

		//                    _mailClient.Send(message);
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                log.Error("Error Approving Driver", ex);

		//                throw;
		//            }
		//        }

		//        public void SetDriverDeclined(int driverId)
		//        {
		//            try
		//            {
		//                var driver = _driverRepository.GetDriverById(driverId);

		//                driver.DateCancelled = DateTime.Now;

		//                _driverRepository.UpdateDriver(driver);

		//                using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
		//                {
		//                    var message = new MailMessage();

		//                    string toAddress = driver.EmailAddress;

		//                    message.To.Add(new MailAddress(toAddress));
		//                    message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen.com Registration");
		//                    message.Subject = "ProgressTen - Club Affiliation";
		//                    message.Body = @"
		//The admins for " + driver.HomeClub.Acronym + @" have decided to decline your request to be included as an active member of their 
		//club. ProgressTen.com has no input or control over this decision. If this decision was in error, any questions will need to be 
		//directed to the managers of " + driver.HomeClub.Acronym + @".
		//
		//Thank you
		//ProgressTen.com";

		//                    _mailClient.Send(message);
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                log.Error("Error Declining Driver", ex);

		//                throw;
		//            }
		//        }
	}
}
