using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RazorStore.Model;
using RazorStore.Services;

namespace RazorStore.Test
{
	public class DbTest
	{
		[Fact]
		public void Test_DbTest_GetGood()
		{
			var connection = new SqliteConnection("DataSource=:memory");
			connection.Open();

			var option = new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlite(connection)
				.Options;

			using(var context = new AppDbContext(option))
			{
				context.Database.EnsureCreated();
				context.AddRange(
					new Goods { Id = 1, Name = "Stas", Price = "123", Subscribe = "Good", Location = "Bar" },
                    new Goods { Id = 2, Name = "Volodya", Price = "1234", Subscribe = "Bad", Location = "Budva" },
                    new Goods { Id = 3, Name = "Ignat", Price = "1235", Subscribe = "Ugly", Location = "Kotor" }
                    );
				context.SaveChanges();

			}
		}
	}
}

