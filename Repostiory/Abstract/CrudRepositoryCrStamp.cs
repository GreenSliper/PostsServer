using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public abstract class CrudRepositoryCrStamp<ContextT, ModelT, KeyT> : CrudRepository<ContextT, ModelT, KeyT>
		where ModelT : CrStampEntity
		where ContextT : DbContext
	{
		public CrudRepositoryCrStamp(ContextT context) : base(context)
		{ }

		public override async Task SaveChanges()
		{
			var changed = context.ChangeTracker.Entries<ModelT>()
				.Where(e => e.State == EntityState.Added);
			foreach (var entity in changed)
				entity.Entity.CreatedTime = DateTime.Now;
			await base.SaveChanges();
		}
	}
}
