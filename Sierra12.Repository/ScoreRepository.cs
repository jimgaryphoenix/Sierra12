using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class ScoreRepository
	{
		private CoreContainer _core;

		public void UpdateScores(IList<Score> scores)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				foreach (var score in scores)
				{
					_core.Scores.Attach(_core.Scores.Single(s => s.ScoreId == score.ScoreId));

					_core.Scores.ApplyCurrentValues(score);

					_core.SaveChanges();
				}
			}
		}

		public void DeleteScores(EntityCollection<Score> scores)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				foreach (var score in scores)
				{
					string query = string.Format("delete from Score where ScoreId = {0}", score.ScoreId);

					_core.ExecuteStoreCommand(query);
				}

				_core.SaveChanges();
			}
		}
	}
}
