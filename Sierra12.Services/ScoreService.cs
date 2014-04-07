using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class ScoreService : ServiceBase
	{
		private readonly ScoreRepository _scoreRepository;
		private readonly ResultRepository _resultRepository;
		private readonly EventRepository _eventRepository;
		private readonly ClassRepository _classRepository;
		private readonly ClubRepository _clubRepository;

		private readonly ResultService _resultService;

		public ScoreService()
		{
			_scoreRepository = new ScoreRepository();
			_resultRepository = new ResultRepository();
			_eventRepository = new EventRepository();
			_classRepository = new ClassRepository();
			_clubRepository = new ClubRepository();
			_resultService = new ResultService();
		}

		public ScoreViewModel GetNewResultViewModel(int eventId, int classId, int clubId)
		{
			var model = new ScoreViewModel();

			var comp = _eventRepository.GetEventById(eventId);

			var compClass = _classRepository.GetClassById(classId);
			var club = _clubRepository.GetClubById(clubId);

			model.ClassName = compClass.Name;
			model.ClubName = club.Acronym;
			model.ClassId = classId;
			model.ClubId = clubId;
			model.EventId = eventId;
			model.EventName = comp.Name;
			model.NumberOfCourses = comp.NumberOfCourses;

			return model;
		}

		public ScoreViewModel GetIndividualResult(int eventId, int driverId, int classId, int clubId)
		{
			var model = new ScoreViewModel();

			var comp = _eventRepository.GetEventById(eventId);

			var compClass = _classRepository.GetClassById(classId);
			var club = _clubRepository.GetClubById(clubId);

			model.ClassName = compClass.Name;
			model.ClubName = club.Acronym;
			model.ClassId = classId;
			model.ClubId = clubId;
			model.EventId = eventId;
			model.EventName = comp.Name;
			model.NumberOfCourses = comp.NumberOfCourses;

			model.Result = _resultRepository.GetIndividualResult(eventId, driverId, classId);

			return model;
		}

		public void SaveNewScores(ScoreViewModel model, FormCollection form)
		{
			//var comp = _eventRepository.GetEventById(model.EventId);

			var result = new Result
			             	{
			             		EventId = model.EventId,
			             		DriverId = model.DriverId,
			             		ClassId = model.ClassId, 
								DateCreated = DateTime.Now, 
								DateModified = DateTime.Now
			             	};

			for (int i = 0; i < model.NumberOfCourses; i ++ )
			{
				int? courseScore = null;
				
				if(!string.IsNullOrEmpty(form["Course_" + (i + 1) + "_Score"]))
				{
					courseScore = int.Parse(form["Course_" + (i + 1) + "_Score"]);
				}

				var score = new Score
				            	{
				            		CourseNumber = i + 1,
									CourseScore = courseScore
				            	};

				if(result.Scores == null)
				{
					result.Scores = new EntityCollection<Score>();
				}

				result.Scores.Add(score);
			}

			_resultRepository.SaveNewScores(result);

			_resultService.AwardPlaceAndPoints(result.EventId, result.ClassId);
		}

		public void UpdateScores(ScoreViewModel model, FormCollection form)
		{
			//var comp = _eventRepository.GetEventById(model.EventId);

			var result = _resultRepository.GetIndividualResult(model.EventId, model.DriverId, model.ClassId);

			for (int i = 0; i < model.NumberOfCourses; i++)
			{
				int? courseScore = null;

				if (!string.IsNullOrEmpty(form["Course_" + (i + 1) + "_Score"]))
				{
					courseScore = int.Parse(form["Course_" + (i + 1) + "_Score"]);
				}

				//var score = new Score
				//{
				//    CourseNumber = i + 1,
				//    CourseScore = courseScore
				//};
				var score = result.Scores.Where(s => s.CourseNumber == (i + 1)).Single();

				score.CourseScore = courseScore;

				//if (result.Scores == null)
				//{
				//    result.Scores = new EntityCollection<Score>();
				//}

				//result.Scores.Add(score);
			}

			_scoreRepository.UpdateScores(result.Scores.ToList());

			_resultService.AwardPlaceAndPoints(result.EventId, result.ClassId);
		}
	}
}
