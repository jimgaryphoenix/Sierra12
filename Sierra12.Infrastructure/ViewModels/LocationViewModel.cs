using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class LocationViewModel : ViewModelBase
	{
		public int LocationId { get; set; }

		[Required(ErrorMessage = "Please include a Name for the Location")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please include a Url for the Location")]
		public string Url { get; set; }

		public Club Club { get; set; }
    }
}