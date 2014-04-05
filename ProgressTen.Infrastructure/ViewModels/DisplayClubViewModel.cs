using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.DataAnnotationExtensions;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class DisplayClubViewModel : ViewModelBase
    {
		public Driver Driver { get; set; }
		public Club Club { get; set; }

		public Series CurrentSeries { get; set; }
		public Event SelectedEvent { get; set; }
		public ResultsViewModel SelectedEventResults { get; set; }
		public StandingsViewModel CurrentSeriesStandings { get; set; }

		public IList<Series> AvailableSeries { get; set; }
		public IList<Club> ClubsApplied { get; set; }
		public IList<Club> ActiveClubs { get; set; }
		public IList<Driver> ApprovedDrivers { get; set; }
		public IList<Driver> DriversApplied { get; set; }

		public SelectList AvailableClubs { get; set; }
    }
}