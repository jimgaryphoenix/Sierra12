using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class ResultsViewModel : ViewModelBase
    {
		public int SelectedEventId { get; set; }

		public Event Event { get; set; }

		public IList<ResultGroupViewModel> Results { get; set; }
    }
}