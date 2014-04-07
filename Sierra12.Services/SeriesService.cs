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
	public class SeriesService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DriverService).Name);

		private readonly SeriesRepository _seriesRepository;
		private readonly ClubRepository _clubRepository;
		private readonly ClassRepository _classRepository;

		private readonly EventService _eventService;
		private readonly ResultService _resultService;

		public SeriesService()
		{
			_seriesRepository = new SeriesRepository();
			_clubRepository = new ClubRepository();
			_classRepository = new ClassRepository();

			_eventService = new EventService();
			_resultService = new ResultService();
		}

		public SeriesViewModel GetNewSeriesViewModel(int clubId)
		{
			var model = new SeriesViewModel();

			model.Club = _clubRepository.GetClubById(clubId);
			model.AllClasses = _classRepository.GetAllClasses();

			return model;
		}

		public SeriesViewModel GetEditSeriesViewModel(int seriesId)
		{
			var model = new SeriesViewModel();

			var series = _seriesRepository.GetSeriesById(seriesId);

			model.SeriesId = series.SeriesId;
			model.Name = series.Name;
			model.BeginDate = series.BeginDate;
			model.Club = series.Club;

			model.AllClasses = _classRepository.GetAllClasses();

			model.SelectedClasses = new int[series.SeriesClasses.Count];

			for (int i = 0; i < series.SeriesClasses.Count; i ++)
			{
				model.SelectedClasses[i] = series.SeriesClasses.ToArray()[i].ClassId;
			}

			return model;
		}

		public SeriesViewModel SaveNewSeries(SeriesViewModel model)
		{
			var series = new Series
			               	{
			               		Name = model.Name,
								BeginDate = model.BeginDate,
			               		ClubId = model.Club.ClubId, 
								DateCreated = DateTime.Now
			               	};

			if (model.SelectedClasses != null && model.SelectedClasses.Length > 0)
			{
				foreach (var classId in model.SelectedClasses)
				{
					var classInSeries = new SeriesClass {SeriesId = model.SeriesId, ClassId = classId};

					if (series.SeriesClasses == null)
					{
						series.SeriesClasses = new EntityCollection<SeriesClass>();
					}

					series.SeriesClasses.Add(classInSeries);
				}
			}

			_seriesRepository.SaveNewSeries(series);

			model.SeriesId = series.SeriesId;

			return model;
		}

		public SeriesViewModel UpdateSeries(SeriesViewModel model)
		{
			var series = _seriesRepository.GetSeriesById(model.SeriesId);

			series.Name = model.Name;
			series.BeginDate = model.BeginDate;

			IList<SeriesClass> newClasses = null;

			if (model.SelectedClasses != null && model.SelectedClasses.Length > 0)
			{
				//series = _seriesRepository.DeleteAllSeriesClasses(series);

				foreach (var classId in model.SelectedClasses)
				{
					var classInSeries = new SeriesClass { SeriesId = model.SeriesId, ClassId = classId };

					if (newClasses == null)
					{
						newClasses = new List<SeriesClass>();
					}

					newClasses.Add(classInSeries);
				}
			}

			_seriesRepository.UpdateSeries(series, newClasses);

			return model;
		}

		public StandingsViewModel GetStandingsForSeries(Series currentSeries)
		{
			var model = new StandingsViewModel();

			foreach (var seriesClass in currentSeries.SeriesClasses)
			{
				var seriesClassResults = _resultService.GetResultsForClassBySeriesId(currentSeries.SeriesId, seriesClass.ClassId);

				var standingsGroup = new StandingsGroupViewModel();

				standingsGroup.ClassId = seriesClass.ClassId;
				standingsGroup.ClassName = seriesClass.Class.Name;

				foreach(var result in seriesClassResults)
				{
					var item = new StandingsItemViewModel();

					item.Driver = result.Driver;
					item.TotalPoints = seriesClassResults.Where(r => r.DriverId == result.DriverId).Sum(p => p.PointsEarned).GetValueOrDefault(0);

					if(standingsGroup.StandingsItems == null)
					{
						standingsGroup.StandingsItems = new List<StandingsItemViewModel>();
					}

					StandingsItemViewModel existingItem = null;

					if (standingsGroup.StandingsItems.Where(i => i.Driver == item.Driver).Count() > 0)
					{
						existingItem = standingsGroup.StandingsItems.Single(i => i.Driver == item.Driver);
					}

					if(existingItem == null)
					{
						standingsGroup.StandingsItems.Add(item);
						//existingItem.TotalPoints += item.TotalPoints;
					}
					//else
					//{
					//    standingsGroup.StandingsItems.Add(item);
					//}
				}

				if(model.Standings == null)
				{
				    model.Standings = new List<StandingsGroupViewModel>();
				}

				model.Standings.Add(standingsGroup);
			}

			model.SelectedSeriesId = currentSeries.SeriesId;

			return model;
		}
	}
}
