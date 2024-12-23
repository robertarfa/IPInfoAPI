using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using IPInfoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IPInfoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<IPAddress> IPAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IPAddress>()
                .ToTable("IPAddress")
                .HasIndex(i => i.IP)
                .IsUnique();

            modelBuilder.Entity<Country>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<IPAddress>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<IPAddress>()
                .Property(i => i.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
