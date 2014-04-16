using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View("Index");
		}

		public ActionResult Classes()
		{
			return View("Classes");
		}

		public ActionResult About()
		{
			return View("About");
		}

		public ActionResult Contact()
		{
			return View("Contact");
		}
	}
}
