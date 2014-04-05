using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgressTen.Infrastructure.ActionFilters;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
    public class ResultsController : Controller
	{
		private readonly ResultService _resultService;

		public ResultsController()
		{
			_resultService = new ResultService();
		}

		[CheckAuthentication(true)]
        public ActionResult Index(int eventId)
        {
        	var results = _resultService.GetResultsDisplay(eventId);
            return View(results);
        }

		[Authorize, CheckAuthentication(true)]
		public ActionResult Edit(int eventId)
		{
			var results = _resultService.GetResultsDisplay(eventId);
			return View(results);
		}
    }
}
