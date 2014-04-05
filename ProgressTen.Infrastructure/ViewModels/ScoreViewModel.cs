using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class ScoreViewModel : ViewModelBase
	{
		public int EventId { get; set; }
		public int ClassId { get; set; }
		public int ClubId { get; set; }
		public string ClassName { get; set; }
		public string ClubName { get; set; }
		public string EventName { get; set; }
		public int NumberOfCourses { get; set; }

		[Required(ErrorMessage = "You have not selected a driver")]
		public int DriverId { get; set; }

		//public Event Event { get; set; }
		public Result Result { get; set; }
	}
}
