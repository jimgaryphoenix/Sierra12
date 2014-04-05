using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class StandingsGroupViewModel : ViewModelBase
    {
		public int ClassId { get; set; }
		public string ClassName { get; set; }

		public IList<StandingsItemViewModel> StandingsItems { get; set; }
    }
}