using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class RegionRepository
	{
		private CoreContainer _core;

		public IList<Region> GetRegions()
		{
			var regions = new List<Region>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				regions = _core.Regions.ToList();
			}

			return regions;
		}
	}
}
