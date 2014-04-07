using System.ComponentModel.DataAnnotations;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class LoginViewModel : ViewModelBase
    {
		[Required(ErrorMessage = "Please log in with your email address")]
		[Email(ErrorMessage = "The email address is not in the correct format")]
        public string EmailAddress { get; set; }

		[Required(ErrorMessage="Please provide a valid password")]
		[StringRange(6, 50, ErrorMessage="Password must be between 6 and 50 characters")]
        public string Password { get; set; }

		public string FailureMessage = "Invalid email address or password.";
    }
}