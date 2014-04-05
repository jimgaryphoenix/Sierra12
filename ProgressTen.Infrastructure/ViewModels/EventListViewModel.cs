using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;

namespace ProgressTen.Infrastructure.ViewModels
{
	public class EventListViewModel : ViewModelBase
	{
		public IList<Event> Events { get; set; }
	}
}
