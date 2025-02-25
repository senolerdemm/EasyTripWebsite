using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyTripProject.Models
{
    public class Context : DbContext
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
    modelBuilder.Entity<Comments>()
        .HasOne(c => c.FreeTravelGuides) // Comments'in bir FreeTravelGuides'ı var
        .WithMany(f => f.Commentss) // FreeTravelGuides'ın birden çok Comments'i var
        .HasForeignKey(c => c.FreeTravelGuidesId); // ForeignKey tanımı

    base.OnModelCreating(modelBuilder);
}

       
    }
}