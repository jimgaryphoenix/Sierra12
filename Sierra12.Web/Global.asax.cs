using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace ProgressTen.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication).Name);

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			RouteTable.Routes.IgnoreRoute("robots.txt");

			//RouteTable.Routes.MapRoute("DisplayEventInfo", "Event/{eventId}", new { controller = "Event", action = "Index", eventId = UrlParameter.Optional });
			//RouteTable.Routes.MapRoute("DisplayResults", "Result/{eventId}", new { controller = "Result", action = "Index", eventId = UrlParameter.Optional });

			//RouteTable.Routes.MapRoute("Admin_Nationals_Index", "Nationals/Admin/{eventId}", new { controller = "Nationals", action = "Index", eventId = UrlParameter.Optional });
			//RouteTable.Routes.MapRoute("Admin_Events_Index", "Event/Admin/{eventId}", new { controller = "Event", action = "Index", eventId = UrlParameter.Optional });
			//RouteTable.Routes.MapRoute("Admin_Results_Edit", "Result/Admin/Edit/{eventId}", new { controller = "Result", action = "Edit", eventId = UrlParameter.Optional });
			//RouteTable.Routes.MapRoute("Admin_Scores_Edit", "Score/Edit/Event/{eventId}/Driver/{driverId}/Class/{classId}",
			//                           new { controller = "Score", action = "Edit", eventId = 0, driverId = 0, classId = 0 });

			RouteTable.Routes.MapRoute("Score_Edit", "Score/Edit/Event/{eventId}/Club/{clubId}/Class/{classId}/Driver/{driverId}",
									   new { controller = "Score", action = "Edit", eventId = 0, clubId = 0, driverId = 0, classId = 0 });
			RouteTable.Routes.MapRoute("Score_Add", "Score/Add/Event/{eventId}/Club/{clubId}/Class/{classId}",
									   new { controller = "Score", action = "Add", eventId = 0, clubId = 0, classId = 0 });

			RouteTable.Routes.MapRoute("Result_Delete", "Result/Delete/{resultId}/Event/{eventId}/Club/{clubId}",
										new { controller = "Result", action = "Delete", resultId = UrlParameter.Optional, eventId = UrlParameter.Optional, clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Result_Edit", "Result/Edit/{eventId}/Club/{clubId}",
										new { controller = "Result", action = "Edit", eventId = UrlParameter.Optional, clubId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Event_Add", "Event/Add/{seriesId}/Club/{clubId}", 
										new { controller = "Event", action = "Add", seriesId = UrlParameter.Optional, clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Event_Edit", "Event/Edit/{eventId}/Club/{clubId}",
										new { controller = "Event", action = "Edit", eventId = UrlParameter.Optional, clubId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Series_Add", "Series/Add/{clubId}", new { controller = "Series", action = "Add", clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Series_Edit", "Series/Edit/{seriesId}/Club/{clubId}",
										new { controller = "Series", action = "Edit", seriesId = UrlParameter.Optional, clubId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Location_Add", "Location/Add/{clubId}", new { controller = "Location", action = "Add", clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Location_Edit", "Location/Edit/{locationId}/Club/{clubId}", 
										new { controller = "Location", action = "Edit", locationId = UrlParameter.Optional, clubId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Driver_ChangePassword", "Driver/ChangePassword/{driverId}", new { controller = "Driver", action = "ChangePassword", driverId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Driver_Edit", "Driver/Edit/{driverId}/Club/{clubId}", new { controller = "Driver", action = "Edit", driverId = UrlParameter.Optional, clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Driver_Decline", "Driver/Decline/{driverId}/Club/{clubId}", 
										new { controller = "Driver", action = "Decline", driverId = UrlParameter.Optional, clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Driver_Approve", "Driver/Approve/{driverId}/Club/{clubId}", 
										new { controller = "Driver", action = "Approve", driverId = UrlParameter.Optional, clubId = UrlParameter.Optional });
			//RouteTable.Routes.MapRoute("Driver_Register", "Driver/Register", new { controller = "Driver", action = "Register" });
			RouteTable.Routes.MapRoute("Driver_Add", "Driver/Add/Club/{clubId}/Event/{eventId}/Class/{classId}", 
										new { controller = "Driver", action = "Add", clubId = UrlParameter.Optional, eventId = UrlParameter.Optional, classId = UrlParameter.Optional });

			RouteTable.Routes.MapRoute("Club_Decline", "Club/Decline/{clubId}", new { controller = "Club", action = "Decline", clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Club_Approve", "Club/Approve/{clubId}", new { controller = "Club", action = "Approve", clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Club_Edit", "Club/Edit/{clubId}", new { controller = "Club", action = "Edit", clubId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Club_Register", "Club/Register", new { controller = "Club", action = "Register" });
			RouteTable.Routes.MapRoute("Club_Series_Event", "Club/{clubId}/Series/{selectedSeriesId}/Event/{selectedEventId}",
										new { controller = "Club", action = "Index", clubId = UrlParameter.Optional, selectedSeriesId = UrlParameter.Optional, selectedEventId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Club_Series", "Club/{clubId}/Series/{selectedSeriesId}", 
										new { controller = "Club", action = "Index", clubId = UrlParameter.Optional, selectedSeriesId = UrlParameter.Optional });
			RouteTable.Routes.MapRoute("Club_Index", "Club/{clubId}",  new {controller = "Club", action = "Index", clubId = UrlParameter.Optional});

			//RouteTable.Routes.MapRoute("Info", "Info", new { controller = "Site", action = "Info" });
			//RouteTable.Routes.MapRoute("Login", "Login", new { controller = "Site", action = "Login" });



			RouteTable.Routes.MapRoute("About", "About", new { controller = "Home", action = "About" });
			RouteTable.Routes.MapRoute("Contact", "Contact", new { controller = "Home", action = "Contact" });
			RouteTable.Routes.MapRoute("Classes", "Classes", new { controller = "Home", action = "Classes" });

			RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

		}

		protected void Application_Start()
		{
			//AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Application_Error()
		{
			log.Error("ProgressTen Error Report", Server.GetLastError());
		}
	}
}