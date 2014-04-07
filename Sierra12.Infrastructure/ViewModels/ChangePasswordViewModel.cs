using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class ChangePasswordViewModel : ViewModelBase
	{
		public int DriverId { get; set; }
		public int ClubId { get; set; }

		[Required(ErrorMessage = "Please provide your old password")]
		[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = "Please provide a valid new password")]
		[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Please confirm your new password")]
		[StringRange(6, 50, ErrorMessage = "Password must be between 6 and 50 characters")]
		public string ConfirmNewPassword { get; set; }

		public MvcHtmlString StatusMessage { get; set; }
    }
}