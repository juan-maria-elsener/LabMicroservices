using Product.Application.DTOs;
using Product.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductItem, ProductDto>();

            CreateMap<ProductCreateDto, ProductItem>()
                .ConstructUsing(dto => new ProductItem(dto.Name, dto.Description, dto.Price, dto.StockQuantity));
        }

    }
}
