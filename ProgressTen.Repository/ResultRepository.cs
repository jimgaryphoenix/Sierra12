using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class ResultRepository
	{
		private CoreContainer _core;

		public Result GetResultById(int resultId)
		{
			var result = new Result();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				result = _core.Results
					.Include("Class")
					.Include("Driver")
					.Include("Scores")
					.Include("Event")
					.Where(r => r.ResultId == resultId)
					.FirstOrDefault();
			}

			return result;
		}

		public IList<Result> GetResultsByEventAndClass(int eventId, int classId)
		{
			IList<Result> results = new List<Result>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				results = _core.Results
					.Include("Class")
					.Include("Driver")
					.Include("Scores")
					.Include("Event")
					.Where(r => r.EventId == eventId && r.ClassId == classId)
					.ToList();
			}

			return results.OrderBy(r => r.TotalScore).ToList();
		}

		public IList<Result> GetResultsForClassBySeriesId(int seriesId, int classId)
		{
			IList<Result> results = new List<Result>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				results = _core.Results
					.Include("Class")
					.Include("Driver")
					.Include("Scores")
					.Include("Event")
					.Where(r => r.Event.SeriesId == seriesId && r.ClassId == classId)
					.ToList();
			}

			return results;
		}

		public Result GetIndividualResult(int eventId, int driverId, int classId)
		{
			var result = new Result();

			using(var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				result = _core.Results
					.Include("Class")
					.Include("Driver")
					.Include("Scores")
					.Include("Event")
					.Where(r => r.EventId == eventId && r.ClassId == classId && r.DriverId == driverId)
					.FirstOrDefault();
			}

			return result;
		}

		public void SaveNewScores(Result result)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Results.AddObject(result);
				_core.SaveChanges();
			}
		}

		public IList<Result> UpdateResultSet(IList<Result> results)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				foreach(var result in results)
				{
					_core.Results.Attach(_core.Results.Single(r => r.ResultId == result.ResultId));

					_core.Results.ApplyCurrentValues(result);

					_core.SaveChanges();
				}
			}

			return results;
		}

		public void DeleteResult(Result result)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				string query = string.Format("delete from Result where ResultId = {0}", result.ResultId);

				_core.ExecuteStoreCommand(query);

				_core.SaveChanges();
			}
		}
	}
}
