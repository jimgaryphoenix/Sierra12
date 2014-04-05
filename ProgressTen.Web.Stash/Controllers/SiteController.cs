using System.Web.Mvc;
using System.Web.Security;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Services;

namespace ProgressTen.Web.Controllers
{
    public class SiteController : BaseController
    {
        #region Constants and Fields

    	private readonly SiteService _siteService;

        #endregion

        #region Constructors and Destructors

		public SiteController()
		{
			_siteService = new SiteService();
		}

        #endregion

        #region Public Methods

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

			//Membership.CreateUser(loginViewModel.EmailAddress, loginViewModel.Password, loginViewModel.EmailAddress);

			if (_siteService.ValidateLogin(loginViewModel))
			{
				FormsAuthentication.SetAuthCookie(loginViewModel.EmailAddress, false);

				return RedirectToRoute("Admin_Nationals_Index");
			}

			ModelState.AddModelError("FailureMessage", loginViewModel.FailureMessage);

			return View("Login", loginViewModel);
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

        #endregion
    }
}