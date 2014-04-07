using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class CountryRepository
	{
		private CoreContainer _core;

		public IList<Country> GetCountries()
		{
			var countries = new List<Country>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				countries = _core.Countries.ToList();
			}

			return countries;
		}
	}
}
