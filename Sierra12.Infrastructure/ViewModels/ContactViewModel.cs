using System.ComponentModel.DataAnnotations;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class ContactViewModel : ViewModelBase
    {
		[Required(ErrorMessage="Please provide a name")]
		[StringRange(3, 50, ErrorMessage="Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        public string RccScreenName { get; set; }

		[Required(ErrorMessage = "Please provide an email address to which we can reply")]
		[Email(ErrorMessage = "The email address is not in the correct format")]
        public string EmailAddress { get; set; }

		[Required(ErrorMessage="Please add some comments to your message")]
		[StringRange(3, 2000, ErrorMessage="Comments must be between 3 and 2000 characters")]
        public string Comments { get; set; }

        public string ThankYouMessage { get; set; }
    }
}