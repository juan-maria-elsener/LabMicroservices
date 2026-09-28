using AutoMapper;
using Product.Application.DTOs;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<ProductItem>(dto);
            await _repository.AddAsync(product);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado");

            product.UpdateDetails(dto.Name, dto.Description, dto.Price);
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado");

            await _repository.DeleteAsync(product);
        }

    }
}
