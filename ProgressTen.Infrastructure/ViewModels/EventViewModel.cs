using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class EventViewModel : ViewModelBase
    {
		public int EventId { get; set; }

		[Required(ErrorMessage="Please provide a name")]
		[StringRange(3, 50, ErrorMessage="Event name must be between 3 and 50 characters")]
        public string Name { get; set; }

		[Required(ErrorMessage="Please select the Event Type")]
		public int EventTypeId { get; set; }

		[DropDownDefaultAsInt(ErrorMessage="Please select the Location for this event")]
		public int LocationId { get; set; }

		[Required(ErrorMessage="Please select the date for this event")]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Please enter the number of courses for this event")]
		[IsNumericGreaterThenZero(ErrorMessage = "The value entered must be a number greater than zero")]
		public int NumberOfCourses { get; set; }

		public int? SeriesId { get; set; }
		public int ClubId { get; set; }

		public SelectList Locations { get; set; }
    }
}