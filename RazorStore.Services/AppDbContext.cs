using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
		{
			
		}
		public DbSet<Goods> Goods { get; set; }
		public DbSet<PathItem> PathItem { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
		{
			base.OnModelCreating(model);

			model.Entity<Goods>().HasQueryFilter(x => !x.Delete);
		}
    }
}

