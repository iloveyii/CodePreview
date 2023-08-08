using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BookStore.Models;


namespace BookStore.Data
{
	public class SqliteContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(LoadConnectionString());
        }

        static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

