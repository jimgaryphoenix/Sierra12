using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class StandingsItemViewModel : ViewModelBase
	{
		public int ClassId { get; set; }
		public int TotalPoints { get; set; }

		public Driver Driver { get; set; }
	}
}
