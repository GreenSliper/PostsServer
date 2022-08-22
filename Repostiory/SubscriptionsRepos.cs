using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public class SubscriptionsRepos<ContextT> : CrudRepository<ContextT, Subscription, string> where ContextT : DbContext
	{
		public SubscriptionsRepos(ContextT context) : base(context) { }
		public override async Task<Subscription> Get(string id)
		{
			return await entities.FirstOrDefaultAsync(x => x.SubscriptionId == id);
		}
	}
}
