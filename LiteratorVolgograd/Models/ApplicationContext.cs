using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiteratorVolgograd.Models
{
	public class ApplicationContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<About> About { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Publication> Publications { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			const string adminRoleGuid = "6f6f6191-bafc-4f12-bf9a-e55f535de420";
			const string adminUserGuid = "9288117a-2d5a-4baa-aed3-20d64a824edc";
			const string adminPassword = "EWww@3$!erw@#efQwr!";


			builder.Entity<IdentityRole>().HasData(new IdentityRole
			{
				Id = adminRoleGuid,
				Name = "admin",
				NormalizedName = "ADMIN"
			});

			builder.Entity<IdentityUser>().HasData(new IdentityUser
			{
				Id = adminUserGuid,
				UserName = "admin",
				NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, adminPassword),
                SecurityStamp = string.Empty
            });

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleGuid,
				UserId = adminUserGuid
			});
		}
	}
}
