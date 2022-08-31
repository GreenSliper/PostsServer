using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public abstract class CrudRepositoryCrUpStamp<ContextT, ModelT, KeyT> : CrudRepository<ContextT, ModelT, KeyT>
		where ModelT : CrUpStampEntity
		where ContextT : DbContext
	{
		public CrudRepositoryCrUpStamp(ContextT context) : base(context)
		{ }

		public override async Task SaveChanges()
		{
			var changed = context.ChangeTracker.Entries<ModelT>()
				.Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
			foreach (var entity in changed)
			{
				entity.Entity.UpdatedTime = DateTime.Now;
				if (entity.State == EntityState.Added)
					entity.Entity.CreatedTime = DateTime.Now;
			}
			await base.SaveChanges();
		}
	}
}
