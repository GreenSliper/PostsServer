using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public class UserRepos<ContextT> : CrudRepository<ContextT, AppUser, string> where ContextT : DbContext
	{
		public UserRepos(ContextT context) : base(context) { }
		public override async Task<AppUser> Get(string id)
		{
			return await entities.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
