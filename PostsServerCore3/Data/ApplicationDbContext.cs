using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostsServerCore3.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		DbSet<AppUser> AppUsers { get; set; }
		DbSet<Subscription> Subscriptions { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Subscription>()
				.HasOne(e => e.SubTarget)
				.WithMany(a=>a.Subscribers)
				.HasForeignKey(x=>x.SubTargetId);
			modelBuilder.Entity<Subscription>()
				.HasOne(e => e.Subscriber)
				.WithMany(a => a.Subscriptions)
				.HasForeignKey(x => x.SubscriberId);
		}
	}
}
