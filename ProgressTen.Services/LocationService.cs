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
	public class LocationService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DriverService).Name);

		private readonly LocationRepository _locationRepository;
		private readonly ClubRepository _clubRepository;

		public LocationService()
		{
			_locationRepository = new LocationRepository();
			_clubRepository = new ClubRepository();
		}

		public LocationViewModel GetNewLocationViewModel(int clubId)
		{
			var model = new LocationViewModel();

			model.Club = _clubRepository.GetClubById(clubId);

			return model;
		}

		public LocationViewModel GetEditLocationViewModel(int locationId)
		{
			var model = new LocationViewModel();

			var location = _locationRepository.GetLocationById(locationId);

			model.LocationId = location.LocationId;
			model.Name = location.Name;
			model.Url = location.Url;
			model.Club = location.Club;

			return model;
		}

		public LocationViewModel SaveNewLocation(LocationViewModel model)
		{
			var location = new Location
			               	{
			               		Name = model.Name,
			               		Url = model.Url,
			               		ClubId = model.Club.ClubId, 
								DateCreated = DateTime.Now
			               	};

			_locationRepository.SaveNewLocation(location);

			return model;
		}

		public LocationViewModel UpdateLocation(LocationViewModel model)
		{
			var location = _locationRepository.GetLocationById(model.LocationId);

			location.Url = model.Url;

			_locationRepository.UpdateLocation(location);

			return model;
		}
	}
}
