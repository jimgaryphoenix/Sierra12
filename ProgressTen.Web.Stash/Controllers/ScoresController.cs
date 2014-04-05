using System;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
	[Authorize]
    public class ScoresController : Controller
    {
		private readonly ResultService _resultService;
		private readonly ScoreService _scoreService;

		public ScoresController()
		{
			_resultService = new ResultService();
			_scoreService = new ScoreService();
		}

		[CheckAuthentication(true)]
        public ActionResult Edit(int eventId, int driverId, int classId)
        {
			//TODO: Move call to scoreService
        	var result = _resultService.GetIndividualResult(eventId, driverId, classId);

            return View(result);
        }

		[HttpPost, CheckAuthentication(true)]
		public ActionResult Edit(FormCollection form)
		{
			int eventId = Convert.ToInt32(form["eventId"]);
			int driverId = Convert.ToInt32(form["driverId"]);
			int classId = Convert.ToInt32(form["classId"]);

			var model = _resultService.GetIndividualResult(eventId, driverId, classId);

			try
			{
				foreach (var score in model.Result.Scores)
				{
					string formName = string.Format("Course_{0}", score.CourseNumber);

					if (!string.IsNullOrEmpty(form[formName]))
					{
						score.CourseScore = Convert.ToInt32(form[formName]);

						//if(score.CourseScore < -20)
						//{
						//    throw new Exception("Score cannot be less than -20");
						//}

						if(score.CourseScore > 50)
						{
							throw new Exception("No score can be higher than 50. If competition is ended and a driver has failed to start a course, enter 50");
						}

						if(score.CourseScore > 40 && score.CourseScore < 50)
						{
							throw new Exception(
								"There can be no score between 40 and 50.<br/>A score of 40 means a driver started a course but fialed to gain any progress.<br/>A score of 50 means a driver failed to start a course.");
						}
					}
				}

				_scoreService.SaveScores(model);

				ViewData.Model = model;

				return Redirect(string.Format("~/Results/Admin/Edit/{0}", eventId));
				//return RedirectToAction("Index", "Results/Admin", eventId);
			}
			catch(Exception ex)
			{
				ModelState.AddModelError("ErrorMessage", ex.Message);
			}

			return View(model);
		}
    }
}
