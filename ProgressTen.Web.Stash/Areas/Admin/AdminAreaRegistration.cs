using System.Web.Mvc;

namespace ProgressTen.Web.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Admin_Nationals_Default",
				"Admin/{controller}/{action}",
				new { controller = "Nationals", action = "Index" }
			);
		}
	}
}
