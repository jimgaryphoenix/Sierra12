using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;

namespace ProgressTen.Repository
{
	public class ClubRepository
	{
		private CoreContainer _core;

		public Club UpdateClub(Club club)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Clubs.Attach(_core.Clubs.Single(c => c.ClubId == club.ClubId));

				_core.Clubs.ApplyCurrentValues(club);

				_core.SaveChanges();
			}

			return club;
		}

		public Club SaveNewClub(Club club)
		{
			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				_core.Clubs.AddObject(club);
				_core.SaveChanges();
			}

			return club;
		}

		public Club GetClubById(int clubId)
		{
			var club = new Club();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				club = _core.Clubs
						.Include("CurrentPresident")
						.Include("Locations")
						.Include("RegisteringDriver")
						.Where(c => c.ClubId == clubId)
						.FirstOrDefault();
			}

			return club;
		}

		public IList<Club> GetAppliedClubs()
		{
			var clubs = new List<Club>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				clubs = _core.Clubs
						.Include("RegisteringDriver")
						.Where(c => !c.DateActivated.HasValue && !c.DateCancelled.HasValue)
						.ToList();
			}

			return clubs;
		}

		public IList<Club> GetActiveClubs()
		{
			var clubs = new List<Club>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				clubs = _core.Clubs
						.Include("CurrentPresident")
						.Where(c => c.DateActivated.HasValue && !c.DateCancelled.HasValue)
						.ToList();
			}

			return clubs;
		}

		public IList<Club> GetClubAutoComplete(string searchText)
		{
			var clubs = new List<Club>();

			using (var connection = new EntityConnection("name=CoreContainer"))
			{
				_core = new CoreContainer(connection);

				if(searchText.Equals("**"))
				{
					clubs = _core.Clubs
									.Where(c => c.DateActivated.HasValue
												&& !c.DateCancelled.HasValue)
									.OrderBy(c => c.FullName)
									.ToList();
				}
				else
				{
					clubs = _core.Clubs
									.Where(c => c.DateActivated.HasValue
												&& !c.DateCancelled.HasValue
												&& (c.FullName.ToLower().Contains(searchText)
												|| c.Acronym.ToLower().Contains(searchText)))
									.OrderBy(c => c.FullName)
									.ToList();
				}
			}

			return clubs;
		}
	}
}
