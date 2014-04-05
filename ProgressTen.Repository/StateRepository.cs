using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class StateRepository
	{
		private CoreContainer _core;

		public IList<State> GetStates()
		{
			var states = new List<State>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				states = _core.States.ToList();
			}

			return states;
		}
	}
}
