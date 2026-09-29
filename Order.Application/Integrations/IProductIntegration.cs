using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Integrations
{
    public interface IProductIntegration
    {
        Task<ProductInfoDto?> GetProductByIdAsync(int id);
        Task UpdateStockAsync(int productId, int newStock);
    }
}
