using AutoMapper;
using Order.Application.DTOs;
using Order.Application.Integrations;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerIntegration _customerIntegration;
        private readonly IProductIntegration _productIntegration;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerIntegration customerIntegration,
            IProductIntegration productIntegration,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerIntegration = customerIntegration;
            _productIntegration = productIntegration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto dto)
        {
            var customer = await _customerIntegration.GetCustomerByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new Exception($"El cliente con ID {dto.CustomerId} no existe.");
            }

            var order = new OrderEntity(customer.Id, customer.Name);

            foreach (var itemDto in dto.Items)
            {
                var product = await _productIntegration.GetProductByIdAsync(itemDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"El producto con ID {itemDto.ProductId} no existe.");
                }

                if (product.StockQuantity < itemDto.Quantity)
                {
                    throw new Exception($"Stock insuficiente para el producto '{product.Name}'. Stock actual: {product.StockQuantity}.");
                }

                order.AddItem(product.Id, product.Name, product.Price, itemDto.Quantity);

                int newStock = product.StockQuantity - itemDto.Quantity;
                await _productIntegration.UpdateStockAsync(product.Id, newStock);
            }

            await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderDto>(order);
        }

    }
}
