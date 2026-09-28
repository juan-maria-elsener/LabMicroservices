using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<CustomerEntity?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddAsync(CustomerEntity customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerEntity customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CustomerEntity customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

    }
}
