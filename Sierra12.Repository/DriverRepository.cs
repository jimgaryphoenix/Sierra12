using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class DriverRepository
	{
		private CoreContainer _core;

		public Driver UpdateDriver(Driver driver)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Drivers.Attach(_core.Drivers.Single(c => c.DriverId == driver.DriverId));

				_core.Drivers.ApplyCurrentValues(driver);

				_core.SaveChanges();
			}

			return driver;
		}

		public Driver SaveNewDriver(Driver driver)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Drivers.AddObject(driver);
				_core.SaveChanges();
			}

			return driver;
		}

		public Driver GetDriverById(int driverId)
		{
			var driver = new Driver();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				driver = _core.Drivers.Where(d => d.DriverId == driverId).FirstOrDefault();
			}

			return driver;
		}

		public Driver GetDriverByEmailAddress(string emailAddress)
		{
			var driver = new Driver();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				driver = _core.Drivers
								.Include("HomeClub")
								.Where(d => d.EmailAddress == emailAddress)
								.FirstOrDefault();
			}

			return driver;
		}

		public IList<Driver> GetApprovedDrivers(int clubId)
		{
			var drivers = new List<Driver>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				drivers = _core.Drivers.Where(d => d.HomeClubId == clubId && d.DateActivated.HasValue && !d.DateCancelled.HasValue).ToList();
			}

			return drivers;
		}

		public IList<Driver> GetAppliedDrivers(int clubId)
		{
			var drivers = new List<Driver>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				drivers = _core.Drivers.Where(d => d.HomeClubId == clubId && !d.DateActivated.HasValue && !d.DateCancelled.HasValue).ToList();
			}

			return drivers;
		}

		public IList<Driver> GetDriverAutoComplete(string searchText, int maxResults, int clubId, bool clubOnly)
		{
			var drivers = new List<Driver>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				if(clubOnly)
				{
					drivers = _core.Drivers
									.Where(d => d.HomeClubId == clubId 
												&& d.DateActivated.HasValue
												&& !d.DateCancelled.HasValue
												&& (d.FirstName.ToLower().Contains(searchText)
												|| d.LastName.ToLower().Contains(searchText)
												|| d.RccScreenName.ToLower().Contains(searchText)
												|| (d.FirstName + " " + d.LastName).ToLower().Contains(searchText)))
									.OrderBy(d => d.FirstName)
									.ToList();
				}
				else
				{
					drivers = _core.Drivers
									.Where(d => !d.DateCancelled.HasValue
												&& (d.FirstName.ToLower().Contains(searchText)
												|| d.LastName.ToLower().Contains(searchText)
												|| d.RccScreenName.ToLower().Contains(searchText)
												|| (d.FirstName + " " + d.LastName).ToLower().Contains(searchText)))
									.OrderBy(d => d.FirstName)
									.ToList();
				}
			}

			return drivers;
		}

		public Driver GetDriverByScreenName(string screenName)
		{
			var driver = new Driver();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				driver = _core.Drivers.Where(d => d.RccScreenName == screenName).FirstOrDefault();
			}

			return driver;
		}

		//public IList<Driver> GetClubAdmins(int clubId)
		//{
		//    var admins = new List<Driver>();

		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        admins = _core.Drivers.Where(d => /*d.HomeClubId == clubId && d.DateActivated.HasValue && !d.DateCancelled.HasValue*/).ToList();
		//    }

		//    return admins;
		//}
	}
}
