using Microsoft.EntityFrameworkCore;
using Customer.Domain.Entities;

namespace Customer.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);

                entity.OwnsOne(e => e.Address, a =>
                {
                    a.Property(p => p.Street).HasColumnName("Street").HasMaxLength(100);
                    a.Property(p => p.City).HasColumnName("City").HasMaxLength(100);
                    a.Property(p => p.Country).HasColumnName("Country").HasMaxLength(50);
                });
            });
        }
    }
}