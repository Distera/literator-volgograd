﻿using Microsoft.EntityFrameworkCore;

namespace LiteratorVolgograd.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<News> News { get; set; }
      
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=literator;Username=postgres;Password=postgres");
        }
    }
}
