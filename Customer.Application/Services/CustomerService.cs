using AutoMapper;
using Customer.Application.DTOs;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Customer.Domain.ValueObjects;

namespace Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto dto)
        {
            var addressVO = new AddressVO(dto.Street, dto.City, dto.Country);

            var customer = new CustomerEntity(dto.Name, dto.Email, addressVO);

            await _repository.AddAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, CustomerUpdateDto dto)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) throw new KeyNotFoundException();

            var addressVO = new AddressVO(dto.Street, dto.City, dto.Country);
            customer.UpdateDetails(dto.Name, dto.Email, addressVO);

            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {

            var customer = await _repository.GetByIdAsync(id);

            if (customer != null)
            {
                await _repository.DeleteAsync(customer);
            }
        }
    }
}