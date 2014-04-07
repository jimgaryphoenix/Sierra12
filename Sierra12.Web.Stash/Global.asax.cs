using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using log4net.Config;
using Spark;
using Spark.Web.Mvc;

namespace ProgressTen.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication).Name);

		public static void RegisterRoutes(RouteCollection routes)
		{
			//AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			RouteTable.Routes.IgnoreRoute("robots.txt");

			RouteTable.Routes.MapRoute("About", "About", new { controller = "Site", action = "About" });
			RouteTable.Routes.MapRoute("Contact", "Contact", new { controller = "Site", action = "Contact" });
			RouteTable.Routes.MapRoute("Info", "Info", new {controller = "Site", action = "Info"});
			RouteTable.Routes.MapRoute("Login", "Login", new { controller = "Site", action = "Login" });
			RouteTable.Routes.MapRoute("Logout", "Logout", new { controller = "Site", action = "Logout" });

			RouteTable.Routes.MapRoute("DisplayEventInfo", "Events/{eventId}", new { controller = "Events", action = "Index", eventId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("DisplayResults", "Results/{eventId}", new { controller = "Results", action = "Index", eventId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Admin_Nationals_Index", "Nationals/Admin/{eventId}", new { controller = "Nationals", action = "Index", eventId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Admin_Events_Index", "Events/Admin/{eventId}", new { controller = "Events", action = "Index", eventId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Admin_Results_Edit", "Results/Admin/Edit/{eventId}", new { controller = "Results", action = "Edit", eventId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Admin_Scores_Edit", "Scores/Edit/Event/{eventId}/Driver/{driverId}/Class/{classId}",
			                           new {controller = "Scores", action = "Edit", eventId = 0, driverId = 0, classId = 0});

			RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}

		protected void Application_BeginRequest()
		{
			log.Debug("Here!");
		}

		protected void Application_Start()
		{
			XmlConfigurator.Configure();

			RegisterRoutes(RouteTable.Routes);

			ViewEngines.Engines.Clear();

			var settings = new SparkSettings()
				.SetDebug(true)
				.AddAssembly("ProgressTen.Web")
				.AddAssembly("ProgressTen.Infrastructure")
				.AddNamespace("System")
				.AddNamespace("System.Collections.Generic")
				.AddNamespace("System.Linq.Expressions")
				.AddNamespace("System.Text")
				.AddNamespace("System.Web.Mvc")
				.AddNamespace("System.Web.Mvc.Html")
				.AddNamespace("System.Web.Routing")
				.AddNamespace("ProgressTen.Infrastructure.ViewModels")
				.AddNamespace("ProgressTen.Web.Controllers")
				.AddNamespace("ProgressTen.Web.HtmlHelpers");

			ViewEngines.Engines.Add(new SparkViewFactory(settings));
		}

		protected void Application_Error()
		{
			log.Error("Crap!", Server.GetLastError());
		}
	}
}