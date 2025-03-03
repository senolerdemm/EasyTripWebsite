using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EasyTripProject.Models;
namespace EasyTripProject.Models
{
    public class Context : IdentityDbContext<Admin>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Home>? Homes { get; set; }
        public DbSet<About>? Abouts { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Adress>? Adresses { get; set; }
        public DbSet<Comments>? Comments { get; set; }
        public DbSet<FreeTravelGuides>? FreeTravelGuides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasDefaultSchema("public");

            // Explicitly map Identity tables
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
            });
        }
    }
}