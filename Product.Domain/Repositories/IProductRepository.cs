using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductItem>> GetAllAsync();
        Task<ProductItem?> GetByIdAsync(int id);
        Task AddAsync(ProductItem product);
        Task UpdateAsync(ProductItem product);
        Task DeleteAsync(ProductItem product);

    }
}
