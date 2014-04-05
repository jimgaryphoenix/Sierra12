using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class ClassRepository
	{
		private CoreContainer _core;

		public IList<Class> GetAllClasses()
		{
			var classes = new List<Class>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				classes = _core.Classes.OrderBy(c => c.DisplayOrder).ToList();
			}

			return classes;
		}

		public Class GetClassById(int classId)
		{
			var compClass = new Class();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				compClass = _core.Classes.Where(c => c.ClassId == classId).Single();
			}

			return compClass;
		}

		//public Series SaveNewSeries(Series series)
		//{
		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        _core.Series.AddObject(series);
		//        _core.SaveChanges();
		//    }

		//    return series;
		//}

		//public Series UpdateSeries(Series series)
		//{
		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        _core.Series.Attach(_core.Series.Single(s => s.SeriesId == series.SeriesId));

		//        _core.Series.ApplyCurrentValues(series);

		//        _core.SaveChanges();
		//    }

		//    return series;
		//}

		//public Series GetSeriesById(int seriesId)
		//{
		//    var series = new Series();

		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        series = _core.Series
		//                .Include("Events")
		//                .Include("Club")
		//                .Where(s => s.SeriesId == seriesId)
		//                .FirstOrDefault();
		//    }

		//    return series;
		//}

		//public IList<Series> GetAllAvailableSeries(int clubId)
		//{
		//    var allAvailableSeries = new List<Series>();

		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        allAvailableSeries = _core.Series
		//                .Include("Events.Location")
		//                .Include("Events.Results")
		//                .Where(s => s.ClubId == clubId && !s.DateCancelled.HasValue).ToList();
		//    }

		//    return allAvailableSeries;
		//}
	}
}
