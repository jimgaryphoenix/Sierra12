using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressTen.Domain.Entities
{
	public partial class Club
	{
		private string _displayName;

		public string DisplayName
		{
			get { return string.IsNullOrEmpty(_displayName) ? string.Format("{0} ({1})", _FullName, _Acronym) : _displayName; }
			set { _displayName = value; }
		}

		public int CurrentSeriesId { get; set; }
	}
}
