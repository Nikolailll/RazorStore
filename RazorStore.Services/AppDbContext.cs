using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
		{
			
		}
		public DbSet<Goods> Goods { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
		{
			base.OnModelCreating(model);

			model.Entity<Goods>().HasQueryFilter(x => !x.Delete);
		}
    }
}

