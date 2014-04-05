using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
namespace ProgressTen.Domain.Entities
// ReSharper restore CheckNamespace
{
	public partial class Result
	{
		public int? TotalScore
		{
			get 
			{ 
				int total = 0;

				foreach(var score in Scores)
				{
					total += score.CourseScore.GetValueOrDefault(50);
				}

				return total;
			}
		}

		public int? GetCourseXScore(int courseNumber)
		{
			int? courseScore = null;

			var score = Scores.Where(s => s.CourseNumber == courseNumber).Single();

			courseScore = score.CourseScore;

			return courseScore;
		}
	}
}
