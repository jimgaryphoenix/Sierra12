using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using System.Web.Mvc;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class ServiceBase
	{
		private readonly StateRepository _stateRepository;
		private readonly RegionRepository _regionRepository;
		private readonly CountryRepository _countryRepository;
		private readonly ClubRepository _clubRepository;
		private readonly DriverRepository _driverRepository;
		private readonly LocationRepository _locationRepository;

		public ServiceBase()
		{
			_stateRepository = new StateRepository();
			_regionRepository = new RegionRepository();
			_countryRepository = new CountryRepository();
			_clubRepository = new ClubRepository();
			_driverRepository = new DriverRepository();
			_locationRepository = new LocationRepository();
		}

		public int GetHomeClubIdForDriver(string emailAddress)
		{
			var driver = _driverRepository.GetDriverByEmailAddress(emailAddress);

			return driver.HomeClubId.HasValue && driver.DateActivated.HasValue ? driver.HomeClubId.Value : 0;
		}

		public bool CheckUniqueEmail(string emailAddress)
		{
			var driver = _driverRepository.GetDriverByEmailAddress(emailAddress);

			if(driver == null)
			{
				return true;
			}

			return false;
		}

		public bool CheckUniqueScreenName(string screenName)
		{
			var driver = _driverRepository.GetDriverByScreenName(screenName);

			if (driver == null)
			{
				return true;
			}

			return false;
		}

		public SelectList GetStateSelectList()
		{
			var fullStates = _stateRepository.GetStates();
			var stateItems = fullStates.Select(item => new SelectListItem {Text = item.Name, Value = item.Code}).ToList();

			stateItems.Insert(0, new SelectListItem{Text = "--SELECT--", Value="-1"});
			
			return new SelectList(stateItems, "Value", "Text");
		}

		public SelectList GetRegionSelectList()
		{
			var regions = _regionRepository.GetRegions();
			var regionItems = regions.Select(item => new SelectListItem { Text = item.Name, Value = item.RegionId.ToString() }).ToList();

			regionItems.Insert(0, new SelectListItem { Text = "--SELECT--", Value = "-1" });

			return new SelectList(regionItems, "Value", "Text");
		}

		public SelectList GetCountrySelectList()
		{
			var countries = _countryRepository.GetCountries();
			var countryItems = countries.Select(item => new SelectListItem { Text = item.Name, Value = item.Code }).ToList();

			countryItems.Insert(0, new SelectListItem { Text = "--SELECT--", Value = "-1" });

			return new SelectList(countryItems, "Value", "Text");
		}

		public SelectList GetClubSelectList(bool addNotListed)
		{
			var clubs = _clubRepository.GetActiveClubs();
			var clubItems = clubs.Select(item => new SelectListItem { Text = item.DisplayName, Value = item.ClubId.ToString() }).ToList();

			if (addNotListed)
			{
				clubItems.Insert(0, new SelectListItem {Text = "NOT YET LISTED", Value = "-2"});
			}

			clubItems.Insert(0, new SelectListItem {Text = "--SELECT--", Value = "-1", Selected = true });

			return new SelectList(clubItems, "Value", "Text");
		}

		public SelectList GetLocationSelectList(int clubId)
		{
			var fullLocations = _locationRepository.GetLocationsByClubId(clubId).OrderBy(l => l.Name);
			var locationItems = fullLocations.Select(item => new SelectListItem { Text = item.Name, Value = item.LocationId.ToString() }).ToList();

			locationItems.Insert(0, new SelectListItem { Text = "--SELECT--", Value = "-1" });

			return new SelectList(locationItems, "Value", "Text");
		}
	}
}
