using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class ViewModelBase
    {
		public Driver LoggedInDriver { get; set; }
		public bool IsAuthenticated { get; set; }
		public string ErrorMessage { get; set; }
    }
}