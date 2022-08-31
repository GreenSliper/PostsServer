using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public class PostRepos<ContextT> : CrudRepositoryCrUpStamp<ContextT, Post, string> where ContextT : DbContext
	{
		public PostRepos(ContextT context) : base(context) { }

		public override Task<Post> Get(string id)
		{
			return entities.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
