using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class ResultService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ClubService).Name);

		private readonly ResultRepository _resultRepository;
		private readonly EventRepository _eventRepository;
		private readonly ScoreRepository _scoreRepository;

		public ResultService()
		{
			_resultRepository = new ResultRepository();
			_eventRepository = new EventRepository();
			_scoreRepository = new ScoreRepository();
		}

		public ResultsViewModel GetResultsDisplayByEventId(int eventId)
		{
			var model = new ResultsViewModel();

			model.SelectedEventId = eventId;
			model.Event = _eventRepository.GetEventById(eventId);

			foreach (var seriesClass in model.Event.Series.SeriesClasses)
			{
				var classResults = model.Event.Results.Where(r => r.ClassId == seriesClass.ClassId).ToList();

				var resultGroup = new ResultGroupViewModel();

				resultGroup.ClassId = seriesClass.ClassId;
				resultGroup.ClassName = seriesClass.Class.Name;
				resultGroup.Results = classResults.OrderBy(r => r.Place).ToList();

				if (model.Results == null)
				{
					model.Results = new List<ResultGroupViewModel>();
				}

				model.Results.Add(resultGroup);
			}

			return model;
		}

		public IList<Result> GetResultsForClassBySeriesId(int seriesId, int classId)
		{
			return _resultRepository.GetResultsForClassBySeriesId(seriesId, classId);
		}

		public IList<Result> AwardPlaceAndPoints(int eventId, int classId)
		{
			var results = _resultRepository.GetResultsByEventAndClass(eventId, classId);

			results = BreakTies(results);

			int place = 1;
			int currentPoints = 25;
			int pointsGapCounter = 5;

			foreach (var result in results)
			{
				result.Place = place;
				result.PointsEarned = currentPoints;

				place++;
				currentPoints = currentPoints == 0 ? currentPoints : currentPoints - pointsGapCounter;

				if (pointsGapCounter > 1)
				{
					pointsGapCounter--;
				}
			}

			return _resultRepository.UpdateResultSet(results);
		}

		public IList<Result> BreakTies(IList<Result> results)
		{
			var sortedIdList = new List<Result>();

			foreach (var result in results)
			{
				if (sortedIdList.Contains(result)) continue;

				var tiedResults = results.Where(r => r.TotalScore == result.TotalScore).ToList();

				if (tiedResults.Count == 1)
				{
					sortedIdList.Add(result);
					continue;
				}

				do
				{
					var nextResult = GetNextResult(tiedResults, result.Event.NumberOfCourses);

					if (nextResult == null) break;

					sortedIdList.Add(nextResult);

				} while (tiedResults.Count > 0);
			}

			return sortedIdList;
		}

		private Result GetNextResult(IList<Result> tiedResults, int numberOfCourses)
		{
			var winner = new List<Result>();

			for (int c = numberOfCourses; c > 0; c--)
			{
				winner = tiedResults.Where(tr => tr.GetCourseXScore(c) == tiedResults.Min(m => m.GetCourseXScore(c))).ToList();

				if (winner.Count == 1 || (c == 2 && winner.Count > 1))
				{
					tiedResults.Remove(winner[0]);
					return winner[0];
				}

				if (c == 1 && winner.Count > 1)
				{
					tiedResults.Remove(winner[0]);

					log.Warn("Unbreakable Tie was recorded for eventId: " + winner[0].EventId + ", classId: " + winner[0].ClassId + ".");
					
					return winner[0];
				}
			}

			return null;
		}

		public void DeleteResult(int resultId)
		{
			var result = _resultRepository.GetResultById(resultId);

			_scoreRepository.DeleteScores(result.Scores);

			_resultRepository.DeleteResult(result);
		}
	}
}

		//public IList<int> BreakTies(IList<Result> results)
		//{
		//    var sortedIdList = new List<int>();

		//    foreach (var result in results)
		//    {
		//        if(sortedIdList.Contains(result.ResultId))
		//        {
		//            continue;
		//        }

		//        var tiedResults = results.Where(r => r.TotalScore == result.TotalScore).ToList();

		//        if (tiedResults.Count == 1)
		//        {
		//            sortedIdList.Add(result.ResultId);
		//            continue;
		//        }

		//        int originalTiedCount = tiedResults.Count;

		//        for (int c = result.Event.NumberOfCourses; c > 0; c--)
		//        {
		//            // if tiedResutls.count == 1, add it to sortedList and remove it from tiedResults and break
		//            if (tiedResults.Count == 1)
		//            {
		//                //var addResult = _resultRepository.GetResultById(tiedResults[0].ResultId);
		//                sortedIdList.Add(tiedResults[0].ResultId);
		//                tiedResults.Remove(tiedResults[0]);
		//                break;
		//            }

		//            //var courseScores = tiedResults.Select(t => t.Scores.Where(sc => sc.CourseNumber == c).Single()).ToList();

		//            foreach(var tiedResult in tiedResults)
		//            {
		//                // remove score for course c from courseScores and orderby total
		//                tiedResult.Scores.Remove(tiedResult.Scores.Where(s => s.CourseNumber == c).Single());
		//            }

		//            // reorder by totalScore
		//            //tiedResults = tiedResults.OrderBy(r => r.TotalScore).ToList();

		//            IList<Result> winner = null;

		//            // query for matching results again
		//            foreach (var tiedResult in tiedResults)
		//            {
		//                var newTotalTies = tiedResults.Where(nr => nr.TotalScore == tiedResult.TotalScore).ToList();

		//                // if results.count != tiedResults.count then find high total
		//                if(newTotalTies.Count != tiedResults.Count)
		//                {
		//                    if (tiedResult.Scores.Count == 1)
		//                    {
		//                        winner = tiedResults.Where(nt => nt.TotalScore == tiedResults.Min(min => min.TotalScore)).ToList();
		//                    }
		//                    else
		//                    {
		//                        winner = tiedResults.Where(nt => nt.TotalScore == tiedResults.Max(max => max.TotalScore)).ToList();
		//                    }

		//                    break;
		//                }
		//            }

		//            // remove high total from tiedResults and add it to sortedResults
		//            if (winner != null && winner.Count == 1)
		//            {
		//                //var addResult = _resultRepository.GetResultById(winner[0].ResultId);
		//                sortedIdList.Add(winner[0].ResultId);
		//                tiedResults.Remove(winner[0]);
		//            }
		//        }

		//        if(tiedResults.Count == originalTiedCount)
		//        {
		//            //log unbroken tie
		//        }
		//    }

		//    return sortedIdList;
		//}






		//public EditScoresViewModel GetIndividualResult(int eventId, int driverId, int classId)
		//{
		//    var model = new EditScoresViewModel();
		//    model.Result = _resultRepository.GetIndividualResult(eventId, driverId, classId);

		//    return model;
		//}

		//private IList<OverallResult> CalculateOverall(ResultsViewModel model)
		//{
		//    IList<OverallResult> overall = new List<OverallResult>();

		//    foreach(var result in model.ResultsSuper)
		//    {
		//        int driverId = result.DriverId;

		//        var result19 = model.Results19.Where(r => r.DriverId == driverId).FirstOrDefault();
		//        var result22 = model.Results22.Where(r => r.DriverId == driverId).FirstOrDefault();

		//        if (result19 == null || result22 == null) continue;

		//        var overallResult = new OverallResult();

		//        overallResult.Driver = result.Driver;

		//        overallResult.Place19 = CalculatePlaceInClass(model.Results19, result.DriverId);
		//        overallResult.Place22 = CalculatePlaceInClass(model.Results22, result.DriverId);
		//        overallResult.PlaceSuper = CalculatePlaceInClass(model.ResultsSuper, result.DriverId);

		//        overall.Add(overallResult);
		//    }

		//    return overall.OrderBy(o => o.OverallScore).ToList();
		//}

		//private int CalculatePlaceInClass(IList<Result> results, int driverId)
		//{
		//    int place = 0;

		//    foreach(var result in results)
		//    {
		//        place++;

		//        if (result.DriverId == driverId) return place;
		//    }

		//    return place;
		//}
