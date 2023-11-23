using App.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace App.Infrastructure.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Persons> Persons { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>()
           .Property(u => u.RegisterDate)
           .HasDefaultValueSql("GETDATE()");

            builder.Entity<Persons>()
           .Property(u => u.RegisterDate)
           .HasDefaultValueSql("GETDATE()");
        }
    }
}
