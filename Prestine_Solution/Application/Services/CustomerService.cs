using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            IGenericRepository<Customer> repository,
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllAsync(null, c => c.User);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id, c => c.User);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetCustomerByUserIdAsync(int userId)
        {
            var customer = await _repository.FindAsync(c => c.UserId == userId, c => c.User);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with User ID {userId} not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto)
        {
            // Ensure the User exists before creating a customer
            var userExists = await _userRepository.ExistsAsync(u => u.UserId == createDto.UserId);
            if (!userExists)
                throw new KeyNotFoundException("User does not exist.");

            var customer = _mapper.Map<Customer>(createDto);
            await _repository.CreateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<CustomerDto> CreateCustomerWithUserAsync(CreateCustomerUserDto createDto)
        {
            // Check if the user with this email already exists
            bool userExists = await _userRepository.ExistsAsync(u => u.Email.ToLower() == createDto.Email.ToLower());
            if (userExists)
                throw new InvalidOperationException("A user with this email already exists.");

            // Create user
            var user = new User
            {
                Username = createDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password),
                Email = createDto.Email,
                Phone = createDto.Phone,
                FullName = createDto.FullName,
                Role = (int?)UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.CreateAsync(user);

            // Create customer linked to the new user
            var customer = new Customer
            {
                UserId = user.UserId,
                Preferences = createDto.Preferences,
                MedicalHistory = createDto.MedicalHistory,
                LastVisitAt = createDto.LastVisitAt
            };
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
            var customers = await _repository.GetAllAsync(
                c => c.LastVisitAt.HasValue && c.LastVisitAt.Value.Date == date.Date,
                c => c.User
            );

            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task UpdateLastVisitDateAsync(int id, DateTime lastVisitDate)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            customer.LastVisitAt = lastVisitDate;
            await _repository.UpdateAsync(customer);
        }
    }
}
