using Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity?> GetByIdAsync(int id);
        Task AddAsync(CustomerEntity customer);
        Task UpdateAsync(CustomerEntity customer);
        Task DeleteAsync(CustomerEntity customer);

    }
}
