using System.Web.Mvc;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Web.Controllers
{
	[HandleError]
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View("Index", new ViewModelBase());
		}
	}
}
