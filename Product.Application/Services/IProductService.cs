using Product.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(int id, ProductUpdateDto dto);
        Task DeleteAsync(int id);

    }
}
