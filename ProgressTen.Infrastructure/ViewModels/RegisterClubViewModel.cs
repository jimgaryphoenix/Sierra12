using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class RegisterClubViewModel : ViewModelBase
    {
		public int ClubId { get; set; }

		[Required(ErrorMessage = "Please provide the full name of your club")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Please include your club name's acronym")]
		public string Acronym { get; set; }

		[Required(ErrorMessage = "Please provide the City that is the base for your club")]
		public string City { get; set; }

		[DropDownDefaultAsString(ErrorMessage = "Please select the state that your club represents")]
		public string State { get; set; }

		[DropDownDefaultAsInt(ErrorMessage = "Please select the region in which your club participates in National Championship events")]
		public int RegionId { get; set; }

		[DropDownDefaultAsString(ErrorMessage = "Please select the Country where your club is located")]
		public string Country { get; set; }

		[Required(ErrorMessage = "Please include the First Name of your club's president")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please include the Last Name of your club's president")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please include the rccrawler.com screen name of your club's president")]
		public string ScreenName { get; set; }

		[Required(ErrorMessage = "Please include the email address of your club's president")]
		[Email(ErrorMessage = "The email address is not in the correct format")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Please provide a valid password")]
		[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please confirm your password")]
		[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		public string ConfirmPassword { get; set; }

		public MvcHtmlString StatusMessage { get; set; }

		public SelectList States { get; set; }
		public SelectList Regions { get; set; }
		public SelectList Countries { get; set; }
    }
}