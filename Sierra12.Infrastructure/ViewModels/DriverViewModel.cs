using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class DriverViewModel : ViewModelBase
	{
		public int DriverId { get; set; }
		public int? EventId { get; set; }
		public int? ClassId { get; set; }
		public int? ClubId { get; set; }

		//[Required(ErrorMessage = "Please include your email address")]
		[Email(ErrorMessage = "The email address is not in the correct format")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Driver's first name is required")]
		public string FirstName { get; set; }

		//[Required(ErrorMessage = "Please include you Last Name")]
		public string LastName { get; set; }

		//[Required(ErrorMessage = "Please include your rccrawler.com screen name")]
		public string ScreenName { get; set; }

		//[Required(ErrorMessage = "Please provide the City in which this driver lives")]
		public string City { get; set; }

		//[DropDownDefaultAsString(ErrorMessage = "Please select the state in which you live")]
		public string State { get; set; }

		[DropDownSelectedNullableInt(ErrorMessage = "Please select a Home Club with which this driver normally competes")]
		public int HomeClubId { get; set; }

		//[Required(ErrorMessage = "Please provide a valid password")]
		//[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		//public string Password { get; set; }

		//[Required(ErrorMessage = "Please confirm your password")]
		//[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		//public string ConfirmPassword { get; set; }

		public bool ChangeHomeClubId { get; set; }

		public MvcHtmlString StatusMessage { get; set; }

		public SelectList States { get; set; }
		public SelectList Regions { get; set; }
		public SelectList Countries { get; set; }
		public SelectList Clubs { get; set; }
    }
}