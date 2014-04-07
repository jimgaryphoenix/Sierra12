using System.Web.Mvc;
using System.Web.Security;
using log4net;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
    public class SiteController : BaseController
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ClubService).Name);

		private readonly SiteService _siteService;
		private readonly ClubService _clubService;

		public SiteController()
		{
			_siteService = new SiteService();
			_clubService = new ClubService();
		}

        public ActionResult Info()
        {
            return View("Info", new ViewModelBase());
        }

        public ActionResult About()
        {
			return View("About", new ViewModelBase());
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View("Contact", new ContactViewModel());
        }

        [HttpPost, ValidateInput(true)]
        public ActionResult Contact(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", contactViewModel);
            }

			_siteService.SendContactEmail(contactViewModel);

			return View("ContactSuccess", contactViewModel);
        }

		[HttpGet]
		public ActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost, ValidateInput(true)]
		public ActionResult Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				loginViewModel.Password = string.Empty;

				return View("Login", loginViewModel);
			}

			if (_siteService.ValidateLogin(loginViewModel))
			{
				FormsAuthentication.SetAuthCookie(loginViewModel.EmailAddress.ToLower(), false);

				int clubId = _clubService.GetHomeClubIdForDriver(loginViewModel.EmailAddress.ToLower());

				return Redirect(string.Format("http://{0}/Club/{1}", Request.Url.Authority, clubId));
			}

			ModelState.AddModelError("FailureMessage", loginViewModel.FailureMessage);

			return View("Login", loginViewModel);
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View("Register", new ViewModelBase());
		}

		public ActionResult ResetPassword()
		{
			return View(new LoginViewModel{Password = "dummyValue"});
		}

		[HttpPost, ValidateInput(true)]
		public ActionResult ResetPassword(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginViewModel);
			}

			if(_siteService.ResetPassword(loginViewModel))
			{
				return View("ResetPasswordSuccess", loginViewModel);
			}

			ModelState.AddModelError("FailureMessage", loginViewModel.FailureMessage);

			return View(loginViewModel);
		}
    }
}