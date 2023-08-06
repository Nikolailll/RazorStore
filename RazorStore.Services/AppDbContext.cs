using System;
using Microsoft.EntityFrameworkCore;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
		{
			
		}
		public DbSet<Goods> Goods { get; set; }

		
	}
}

