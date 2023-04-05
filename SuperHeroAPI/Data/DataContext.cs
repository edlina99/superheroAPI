using System;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Log> Logs { get; set; }
	}
}

