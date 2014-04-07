using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;

namespace ProgressTen.Repository
{
	public class MessageRepository
	{
		private CoreContainer _core;

		public void SaveMessage(Message message)
		{
			using (EntityConnection connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);
				_core.Messages.AddObject(message);
				_core.SaveChanges();
			}
		}
	}
}
