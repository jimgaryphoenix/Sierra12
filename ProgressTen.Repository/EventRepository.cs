using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class EventRepository
	{
		private CoreContainer _core;

		public Event GetEventById(int eventId)
		{
			var comp = new Event();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				comp = _core.Events
							.Include("Results.Scores")
							.Include("Results.Driver")
							.Include("Series.SeriesClasses.Class")
							.Where(e => e.EventId == eventId)
							.FirstOrDefault();
			}

			return comp;
		}

		public IList<Event> GetEventsBySeriesId(int seriesId)
		{
			var events = new List<Event>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				events = _core.Events
							.Include("Results.Scores")
							.Include("Results.Driver")
							.Include("Series.SeriesClasses.Class")
							.Where(e => e.SeriesId == seriesId)
							.ToList();
			}

			return events;
		}

		public Event SaveNewEvent(Event comp)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Events.AddObject(comp);
				_core.SaveChanges();
			}

			return comp;
		}

		public Event UpdateEvent(Event comp)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Events.Attach(_core.Events.Single(s => s.EventId == comp.EventId));

				_core.Events.ApplyCurrentValues(comp);

				_core.SaveChanges();
			}

			return comp;
		}

		//public IList<Series> GetAllAvailableSeries(int clubId)
		//{
		//    var series = new List<Series>();

		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        series = _core.Series
		//                .Include("Events.Location")
		//                .Where(s => s.ClubId == clubId && !s.DateCancelled.HasValue).ToList();
		//    }

		//    return series;
		//}

		//public IList<Event> GetUpcomingNationalEvents()
		//{
		//    IList<Event> events = new List<Event>();

		//    using (var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        //DateTime twoWeeksAgo = DateTime.Today.AddDays(-14);

		//        events = _core.Events
		//            .Include("Location")
		//            .Include("Club")
		//            //.Where(e => e.Date > twoWeeksAgo)
		//            .ToList();
		//    }

		//    return events;
		//}

		//public Event GetEventInfoById(int? eventId)
		//{
		//    var comp = new Event();

		//    using(var connection = new EntityConnection("name=CoreContainer"))
		//    {
		//        _core = new CoreContainer(connection);

		//        comp = _core.Events
		//            .Include("Location")
		//            .Include("Club")
		//            .Where(e => e.EventId == eventId)
		//            .FirstOrDefault();
		//    }

		//    return comp;
		//}
	}
}
