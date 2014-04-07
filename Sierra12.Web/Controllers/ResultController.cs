using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Services;
using ProgressTen.Web.ActionFilters;

namespace ProgressTen.Web.Controllers
{
    public class ResultController : BaseController
	{
		private readonly ResultService _resultService;

		public ResultController()
		{
			_resultService = new ResultService();
		}

		[Authorize, SetLoggedInDriver, UserIsClubAdmin]
		public ActionResult Edit(int eventId)
		{
			var model = _resultService.GetResultsDisplayByEventId(eventId);

			return View(model);
		}

		[Authorize, UserIsClubAdmin]
		public ActionResult Delete(int resultId)
		{
			int eventId = int.Parse(RouteData.Values["eventId"].ToString());
			int clubId = int.Parse(RouteData.Values["clubId"].ToString());

			_resultService.DeleteResult(resultId);

			return Redirect("http://" + Request.Url.Authority + "/Result/Edit/" + eventId + "/Club/" + clubId);
		}
    }
}
