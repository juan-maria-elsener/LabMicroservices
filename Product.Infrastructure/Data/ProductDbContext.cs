using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<ProductItem> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");
            });
        }

    }
}
