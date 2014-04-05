using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class LocationRepository
	{
		private CoreContainer _core;

		public Location GetLocationById(int locationId)
		{
			var location = new Location();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				location = _core.Locations
								.Include("Club")
								.Where(l => l.LocationId == locationId)
								.FirstOrDefault();
			}

			return location;
		}

		public IList<Location> GetLocationsByClubId(int clubId)
		{
			var locations = new List<Location>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				locations = _core.Locations.Where(l => l.ClubId == clubId).ToList();
			}

			return locations;
		}

		public Location SaveNewLocation(Location location)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Locations.AddObject(location);
				_core.SaveChanges();
			}

			return location;
		}

		public Location UpdateLocation(Location location)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Locations.Attach(_core.Locations.Single(l => l.LocationId == location.LocationId));

				_core.Locations.ApplyCurrentValues(location);

				_core.SaveChanges();
			}

			return location;
		}
	}
}
