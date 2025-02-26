using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(
            IGenericRepository<Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetCustomerByUserIdAsync(int userId)
        {
            var customers = await _repository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.UserId == userId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with User ID {userId} not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto)
        {
            var customer = _mapper.Map<Customer>(createDto);

            await _repository.CreateAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateCustomerAsync(int id, UpdateCustomerDto updateDto)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found");

            _mapper.Map(updateDto, customer);
            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found");

            await _repository.DeleteAsync(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersByLastVisitDateAsync(DateTime date)
        {
            var customers = await _repository.GetAllAsync();
            var filteredCustomers = customers.Where(c => c.LastVisitAt.HasValue && c.LastVisitAt.Value.Date == date.Date);

            return _mapper.Map<IEnumerable<CustomerDto>>(filteredCustomers);
        }

        public async Task UpdateLastVisitDateAsync(int id, DateTime lastVisitDate)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found");

            customer.LastVisitAt = lastVisitDate;
            await _repository.UpdateAsync(customer);
        }
    }
}
