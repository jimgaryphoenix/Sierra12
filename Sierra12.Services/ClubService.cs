using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects.DataClasses;
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
	public class ClubService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ClubService).Name);

		private readonly ClubRepository _clubRepository;
		private readonly DriverRepository _driverRepository;
		private readonly SeriesRepository _seriesRepository;

		private readonly ResultService _resultService;
		private readonly SeriesService _seriesService;

		public ClubService()
		{
			_clubRepository = new ClubRepository();
			_driverRepository = new DriverRepository();
			_seriesRepository = new SeriesRepository();

			_resultService = new ResultService();
			_seriesService = new SeriesService();
		}

		public RegisterClubViewModel RegisterNewClub(RegisterClubViewModel clubModel)
		{
			var newClub = new Club
								{
									FullName = clubModel.FullName,
									Acronym = clubModel.Acronym,
									City = clubModel.City,
									State = clubModel.State,
									RegionId = clubModel.RegionId,
									Country = clubModel.Country, 
									DateCreated = DateTime.Now
								};

			var newDriver = new Driver
			                	{
			                		FirstName = clubModel.FirstName,
			                		LastName = clubModel.LastName,
			                		RccScreenName = clubModel.ScreenName,
			                		City = clubModel.City,
			                		State = clubModel.State,
			                		EmailAddress = clubModel.EmailAddress.ToLower(), 
			                		DateCreated = DateTime.Now
			                	};

			try
			{
				Membership.CreateUser(newDriver.EmailAddress, clubModel.Password, newDriver.EmailAddress);

				_driverRepository.SaveNewDriver(newDriver);
				_clubRepository.SaveNewClub(newClub);

				newClub.CurrentPresidentDriverId = newDriver.DriverId;
				newClub.RegisteringDriverId = newDriver.DriverId;

				_clubRepository.UpdateClub(newClub);

				newDriver.HomeClubId = newClub.ClubId;

				_driverRepository.UpdateDriver(newDriver);

				//clubModel.ClubId = newClub.ClubId;

				using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
				{
					var message = new MailMessage();

					string toAddress = ConfigurationManager.AppSettings["NewClubToAddress"];

					message.To.Add(new MailAddress(toAddress, "ProgressTen.com Registration"));
					message.From = new MailAddress("noReply@progressten.com", "ProgressTen Registration");
					message.Subject = "ProgressTen - New Club Registration";
					message.Body = @"
A new club has been submitted for consideration with the following information:\n\n
Club Name: " + newClub.FullName + @"\n
Acronym: " + newClub.Acronym + @"\n
City: " + newClub.City + @"\n
State: " + newClub.State + @"\n
RegionId: " + newClub.RegionId + @"\n
Country: " + newClub.Country + @"\n
President's Name: " + newDriver.FullName + @"\n
President's ScreenName: " + newDriver.RccScreenName + @"\n
President's Email: " + newDriver.EmailAddress + @"\n";

					_mailClient.Send(message);
				}

				clubModel.StatusMessage = MvcHtmlString.Create("<p>" + 
											"Thank you for submitting your club as a member club of ProgressTen.com. As you know, clubs must be " +
											"sanctioned, or in the process of becoming sanctioned with the USRCCA. As soon as we are able to " +
											"confirm your status with the USRCCA, we'll notify you of your club's confirmation." + 
											"</p>" + 
											"<p>" + 
											"In the mean time, you are now registered as a Driver in ProgressTen.com and you have access to the " + 
											"public areas that are available to all Drivers who are not yet affiliated with a club. You can log in " + 
											"anytime with the email address and password you provided." + 
											"</p>" + 
											"<p>" +
											"<a href='./" + newClub.ClubId + "' >Go To Club Page</a></p>" +
											"<p>" +
											"<a href='../Login' >Log In</a></p>");
			}
			catch (Exception ex)
			{
				log.Error("Problem Creating New Club for User:" + newDriver.EmailAddress + " Club: " + newClub.FullName, ex);

				Membership.DeleteUser(newDriver.EmailAddress);

				clubModel.StatusMessage = MvcHtmlString.Create("<p>" + 
											"There was a problem submitting this New Club entry. You may double check that all your data was " +
											"entered correctly and try again. ProgressTen.com administrators have been made aware of the " +
											"error and will look into the matter further." + 
											"</p>");
			}

			return clubModel;
		}

		public Club GetClubById(int clubId)
		{
			return _clubRepository.GetClubById(clubId);
		}

		//public ClubViewModel GetClubViewModel(int clubId)
		//{
		//    var clubViewModel = new ClubViewModel();

		//    clubViewModel.Club = GetClubById(clubId);
		//    clubViewModel.Countries = GetCountrySelectList();
		//    clubViewModel.Regions = GetRegionSelectList();
		//    clubViewModel.States = GetStateSelectList();
		//    clubViewModel.Admins = _driverRepository.GetClubAdmins(clubId);

		//    return clubViewModel;
		//}

		public DisplayClubViewModel GetClubDisplayViewModel(int clubId, IIdentity identity, int? selectedSeriesId, int? selectedEventId)
		{
			var model = new DisplayClubViewModel();

			model.Driver = _driverRepository.GetDriverByEmailAddress(identity.Name);
			model.Club = GetClubById(clubId);
			model.AvailableSeries = _seriesRepository.GetAllAvailableSeries(clubId).OrderBy(s => s.BeginDate).ToList();

			if(model.AvailableSeries != null && model.AvailableSeries.Count > 0)
			{
				if(selectedSeriesId.HasValue)
				{
					model.CurrentSeries = model.AvailableSeries.Where(s => s.SeriesId == selectedSeriesId.Value).FirstOrDefault();
				}
				else
				{
					if (model.AvailableSeries.Where(s => s.BeginDate <= DateTime.Now).Count() > 0)
					{
						var maxBeginDate = model.AvailableSeries.Where(s => s.BeginDate <= DateTime.Now).Max(d => d.BeginDate);

						model.CurrentSeries = model.AvailableSeries.Where(s => s.BeginDate == maxBeginDate).FirstOrDefault();
					}
					else
					{
						var seriesDate = model.AvailableSeries.Max(s => s.BeginDate);

						model.CurrentSeries = model.AvailableSeries.Where(s => s.BeginDate == seriesDate).FirstOrDefault();
					}
				}
			}

			if (model.CurrentSeries != null)
			{
				model.CurrentSeriesStandings = _seriesService.GetStandingsForSeries(model.CurrentSeries);
			}

			if (model.CurrentSeries != null && model.CurrentSeries.Events != null && model.CurrentSeries.Events.Count > 0)
			{
				if(selectedEventId.HasValue)
				{
					model.SelectedEvent = model.CurrentSeries.Events.Where(e => e.EventId == selectedEventId).Single();
				}
				else
				{
					if (model.CurrentSeries.Events.Count > 0 && model.CurrentSeries.Events.Where(e => e.Results.Count > 0).Count() > 0)
					{
						var nextDate = model.CurrentSeries.Events.Where(e => e.Results.Count > 0).Max(e => e.Date);

						if(nextDate > DateTime.Today)
						{
							nextDate = model.CurrentSeries.Events.Where(e => e.Results.Count == 0).Min(e => e.Date);
						}

						model.SelectedEvent = model.CurrentSeries.Events.Where(e => e.Date == nextDate).FirstOrDefault();
					}
					else
					{
						model.SelectedEvent = model.CurrentSeries.Events.First();
					}
				}
			}
			else
			{
				model.SelectedEvent = new Event();
			}

			if (model.SelectedEvent.EventId > 0)
			{
				model.SelectedEventResults = _resultService.GetResultsDisplayByEventId(model.SelectedEvent.EventId);
			}

			model.ClubsApplied = _clubRepository.GetAppliedClubs();
			model.ActiveClubs = _clubRepository.GetActiveClubs();
			model.ApprovedDrivers = _driverRepository.GetApprovedDrivers(clubId).OrderBy(d => d.FirstName).ToList();
			//model.DriversApplied = _driverRepository.GetAppliedDrivers(clubId);
			model.AvailableClubs = GetClubSelectList(false);

			return model;
		}

		public void SetClubApproved(int clubId)
		{
			try
			{
				var club = _clubRepository.GetClubById(clubId);

				club.DateActivated = DateTime.Now;
				club.DateCancelled = null;

				club.CurrentPresident.HomeClubId = club.ClubId;
				club.CurrentPresident.DateActivated = DateTime.Now;

				_clubRepository.UpdateClub(club);
				_driverRepository.UpdateDriver(club.CurrentPresident);

				if(!Roles.RoleExists("ClubAdmin_" + clubId))
				{
					Roles.CreateRole("ClubAdmin_" + clubId);
				}

				if (!Roles.IsUserInRole(club.CurrentPresident.EmailAddress, "ClubAdmin_" + clubId))
				{
					Roles.AddUserToRole(club.CurrentPresident.EmailAddress, "ClubAdmin_" + clubId);
				}

				using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
				{
					var message = new MailMessage();

					string toAddress = club.CurrentPresident.EmailAddress;

					message.To.Add(new MailAddress(toAddress));
					message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen.com Registration");
					message.Subject = "ProgressTen - New Club Registration";
					message.Body = @"
Your club has been verified and is ready to start managing your club's activities on ProgressTen.com. You, " + club.CurrentPresident.FullName + @", have been granted permissions 
as the Club Admin for " + club.Acronym + @"s pages on ProgressTen.com. When you next log in, you see will additional tabs on " + club.Acronym + @"'s page and 
links you can use to create competition series, add events to the series, update results, and a tab that only Club Admins can see that allows you to manage the 
roster of drivers in your club.

If you have any questions, feel free to either contact us at help@progressten.com, or post a question in the ""ProgressTen.com Helpdesk"" thread in the Vendor's section on RCCrawler.com

Thanks for your support
ProgressTen.com";

					_mailClient.Send(message);
				}
			}
			catch (Exception ex)
			{
				log.Error("Error Approving Club", ex);

				throw;
			}
		}

		public void SetClubDeclined(int clubId)
		{
			try
			{
				var club = _clubRepository.GetClubById(clubId);

				club.DateCancelled = DateTime.Now;

				club.CurrentPresident.HomeClubId = null;

				_clubRepository.UpdateClub(club);
				_driverRepository.UpdateDriver(club.CurrentPresident);

				if (Roles.IsUserInRole(club.CurrentPresident.EmailAddress, "ClubAdmin"))
				{
					Roles.RemoveUserFromRole(club.CurrentPresident.EmailAddress, "ClubAdmin");
				}

				using (var _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
				{
					var message = new MailMessage();

					string toAddress = club.CurrentPresident.EmailAddress;

					message.To.Add(new MailAddress(toAddress));
					message.From = new MailAddress("doNotReply@progressten.com", "ProgressTen.com Registration");
					message.Subject = "ProgressTen - New Club Registration";
					message.Body = @"
We were unable to verify your club registration. Either we were unable to verify that " + club.Acronym + @" is a valid RC Rock 
Crawling club, or we were unable to determin that you are the authorized contact person create and manage this club on 
ProgressTen.com. If you feel that we are incorrect in our conclusion, feel free to contact us at help@progressten.com or show 
us where on RCCrawler.com we can find the information that will help us resolve this matter.

Thank you
ProgressTen.com";

					_mailClient.Send(message);
				}
			}
			catch (Exception ex)
			{
				log.Error("Error Declining Club", ex);

				throw;
			}
		}

		public IList<Club> GetClubAutoComplete(string searchText)
		{
			var clubs = _clubRepository.GetClubAutoComplete(searchText);

			return clubs.Select(club => new Club { DisplayName = club.DisplayName, ClubId = club.ClubId }).ToList();
		}
	}
}
