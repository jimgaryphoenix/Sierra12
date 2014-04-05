using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class SeriesViewModel : ViewModelBase
	{
		public int SeriesId { get; set; }

		[Required(ErrorMessage = "Please include a Name for the Series")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please select the date this Series begins")]
		public DateTime BeginDate { get; set; }

		public IList<Class> AllClasses { get; set; }

		public int[] SelectedClasses { get; set; }

		public Club Club { get; set; }
    }
}