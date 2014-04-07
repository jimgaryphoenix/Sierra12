using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressTen.Domain.Entities
{
	public class OverallResult
	{
		public Driver Driver { get; set; }
		public int Place19 { get; set; }
		public int Place22 { get; set; }
		public int PlaceSuper { get; set; }
		
		public int OverallScore
		{
			get { return Place19 + Place22 + PlaceSuper; }
		}
	}
}
