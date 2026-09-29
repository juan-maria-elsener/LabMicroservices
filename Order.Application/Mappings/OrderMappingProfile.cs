using AutoMapper;
using Order.Application.DTOs;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderEntity, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }

    }
}
