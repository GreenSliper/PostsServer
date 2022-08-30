using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public class ImageRepos<ContextT> : CrudRepository<ContextT, Image, string> where ContextT : DbContext
	{
		public ImageRepos(ContextT context) : base(context) { }
		public override async Task<Image> Get(string id)
		{
			return await entities.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
