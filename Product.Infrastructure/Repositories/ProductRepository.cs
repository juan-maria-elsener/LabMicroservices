using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

            public ProductRepository(ProductDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ProductItem>> GetAllAsync()
            {
                return await _context.Products.ToListAsync();
            }

            public async Task<ProductItem?> GetByIdAsync(int id)
            {
                return await _context.Products.FindAsync(id);
            }

            public async Task AddAsync(ProductItem product)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(ProductItem product)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(ProductItem product)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

        }
}
