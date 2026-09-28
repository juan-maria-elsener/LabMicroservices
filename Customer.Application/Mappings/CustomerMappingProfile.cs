using AutoMapper;
using Customer.Application.DTOs;
using Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerEntity, CustomerDto>();

            CreateMap<CustomerCreateDto, CustomerEntity>()
                .ConstructUsing(dto => new CustomerEntity(dto.Name, dto.Email, dto.Address));

        }
    }
}
