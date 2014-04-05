using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class EventService : ServiceBase
	{
		private readonly EventRepository _eventRepository;
		private readonly ClubRepository _clubRepository;
		private readonly LocationRepository _locationRepository;

		public EventService()
		{
			_eventRepository = new EventRepository();
			_clubRepository = new ClubRepository();
			_locationRepository = new LocationRepository();
		}

		public EventViewModel GetNewEventViewModel(int clubId, int? seriesId)
		{
			var model = new EventViewModel();

			model.SeriesId = seriesId;
			model.ClubId = _clubRepository.GetClubById(clubId).ClubId;
			model.Date = DateTime.Now;

			if(seriesId.HasValue)
			{
				model.EventTypeId = 1;
			}
			else
			{
				model.EventTypeId = 2;
			}

			model.Locations = GetLocationSelectList(clubId);

			return model;
		}

		public IList<Event> GetEventsBySeriesId(int seriesId)
		{
			return _eventRepository.GetEventsBySeriesId(seriesId);
		}

		public EventViewModel SaveNewEvent(EventViewModel model)
		{
			var comp = new Event
			{
				Name = model.Name,
				Date = model.Date,
				ClubId = model.ClubId,
				SeriesId = model.SeriesId,
				LocationId = model.LocationId,
				EventTypeId = model.EventTypeId, 
				NumberOfCourses = model.NumberOfCourses,
				DateCreated = DateTime.Now
			};

			_eventRepository.SaveNewEvent(comp);

			return model;
		}

		public EventViewModel GetEditEventViewModel(int eventId, int clubId)
		{
			var model = new EventViewModel();

			var comp = _eventRepository.GetEventById(eventId);

			model.EventId = comp.EventId;
			model.Name = comp.Name;
			model.EventTypeId = comp.EventTypeId;
			model.ClubId = clubId;
			model.LocationId = comp.LocationId;
			model.Date = comp.Date;
			model.SeriesId = comp.SeriesId;
			model.NumberOfCourses = comp.NumberOfCourses;

			model.Locations = GetLocationSelectList(clubId);

			return model;
		}

		public EventViewModel UpdateEvent(EventViewModel model)
		{
			var comp = _eventRepository.GetEventById(model.EventId);

			comp.Name = model.Name;
			comp.EventId = model.EventId;
			comp.Name = model.Name;
			comp.EventTypeId = model.EventTypeId;
			comp.LocationId = model.LocationId;
			comp.Date = model.Date;
			comp.SeriesId = model.SeriesId;
			comp.NumberOfCourses = model.NumberOfCourses;

			_eventRepository.UpdateEvent(comp);

			return model;
		}

		//public EventListViewModel GetUpcomingNationalEvents()
		//{
		//    var model = new EventListViewModel();

		//    model.Events = _eventRepository.GetUpcomingNationalEvents();

		//    return model;
		//}

		//public EventViewModel GetEventInfoById(int? eventId)
		//{
		//    var comp = _eventRepository.GetEventInfoById(eventId);

		//    var model = new EventViewModel();

		//    model.EventId = comp.EventId;
		//    model.Name = comp.Name;
		//    model.Date = comp.Date;
		//    model.Location = comp.Location;
		//    model.Club = comp.Club;

		//    return model;
		//}
	}
}
