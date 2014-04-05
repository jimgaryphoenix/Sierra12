using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class StandingsViewModel : ViewModelBase
    {
		public int SelectedSeriesId { get; set; }

		public Series Series { get; set; }

		public IList<StandingsGroupViewModel> Standings { get; set; }
    }
}