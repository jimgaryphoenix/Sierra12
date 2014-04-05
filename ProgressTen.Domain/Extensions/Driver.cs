using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressTen.Domain.Entities
{
	public partial class Driver
	{
		public string FullName
		{
			get { return string.Format("{0} {1}", _FirstName, _LastName); }
		}

		private string _fullDisplayName;

		public string FullDisplayName
		{
			get
			{
				if(string.IsNullOrEmpty(_fullDisplayName))
				{
					_fullDisplayName = string.Format("{0} {1} ({2})", _FirstName, _LastName, !string.IsNullOrEmpty(_RccScreenName) ? _RccScreenName : "none");
				}

				return _fullDisplayName;
			}
			set { _fullDisplayName = value; }
		}
	}
}
