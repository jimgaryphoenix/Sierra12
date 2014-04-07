using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class SeriesRepository
	{
		private CoreContainer _core;

		public Series SaveNewSeries(Series series)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Series.AddObject(series);
				_core.SaveChanges();
			}

			return series;
		}

		public Series UpdateSeries(Series series, IList<SeriesClass> newClasses)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Series.Attach(_core.Series.Include("SeriesClasses").Single(s => s.SeriesId == series.SeriesId));

				_core.Series.ApplyCurrentValues(series);

				if (series.SeriesClasses != null && series.SeriesClasses.Count > 0)
				{
					foreach (var oldClass in series.SeriesClasses)
					{
						string query = string.Format("delete from SeriesClass where SeriesClassId = {0}", oldClass.SeriesClassId);

						_core.ExecuteStoreCommand(query);
					}
				}

				var saveSeries = _core.Series.Single(s => s.SeriesId == series.SeriesId);

				foreach (var selectedClass in newClasses)
				{
					saveSeries.SeriesClasses.Add(new SeriesClass{ClassId = selectedClass.ClassId, SeriesId = selectedClass.SeriesId});
				}

				_core.SaveChanges();
			}

			return series;
		}

		public Series DeleteAllSeriesClasses(Series series)
		{
			foreach (var seriesClass in series.SeriesClasses)
			{
				using (var connection = new EntityConnection("name=CoreContainer"))
				{
					_core = new CoreContainer(connection);

					string query = string.Format("delete from SeriesClass where SeriesClassId = {0}", seriesClass.SeriesClassId);

					_core.ExecuteStoreCommand(query);
				}
			}

			return series;
		}

		public Series GetSeriesById(int seriesId)
		{
			var series = new Series();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				series = _core.Series
						.Include("Club")
						.Include("SeriesClasses")
						.Where(s => s.SeriesId == seriesId)
						.FirstOrDefault();
			}

			return series;
		}

		public IList<Series> GetAllAvailableSeries(int clubId)
		{
			var allAvailableSeries = new List<Series>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				allAvailableSeries = _core.Series
						.Include("Events.Location")
						.Include("Events.Results.Scores")
						.Include("SeriesClasses.Class")
						.Where(s => s.ClubId == clubId && !s.DateCancelled.HasValue).ToList();
			}

			return allAvailableSeries;
		}
	}
}
