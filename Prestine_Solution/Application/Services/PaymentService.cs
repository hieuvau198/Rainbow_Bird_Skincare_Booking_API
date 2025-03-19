﻿using Application.DTOs;
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
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<Payment> _repository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IGenericRepository<Payment> repository,
            IGenericRepository<Transaction> transactionRepository,
            IMapper mapper)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByStatusAsync(string status)
        {
            var payments = await _repository.GetAllAsync();
            var filtered = payments.Where(p => p.Status == status)
                                 .OrderByDescending(p => p.PaymentDate);
            return _mapper.Map<IEnumerable<PaymentDto>>(filtered);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await _repository.GetAllAsync();
            var filtered = payments.Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                                 .OrderByDescending(p => p.PaymentDate);
            return _mapper.Map<IEnumerable<PaymentDto>>(filtered);
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createDto)
        {
            var payment = _mapper.Map<Payment>(createDto);
            payment.PaymentDate = DateTime.UtcNow;

            await _repository.CreateAsync(payment);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task UpdatePaymentAsync(int id, UpdatePaymentDto updateDto)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            _mapper.Map(updateDto, payment);
            await _repository.UpdateAsync(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            await _repository.DeleteAsync(payment);
        }

        // New methods for transactions

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(TransactionFilterDto filter)
        {
            var transactions = await _transactionRepository.GetAllAsync();

            // Apply simple date and amount filters
            if (filter.StartDate.HasValue)
                transactions = transactions.Where(t => t.TransactionDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                transactions = transactions.Where(t => t.TransactionDate <= filter.EndDate.Value);

            if (filter.MinAmount.HasValue)
                transactions = transactions.Where(t => t.Amount >= filter.MinAmount.Value);

            if (filter.MaxAmount.HasValue)
                transactions = transactions.Where(t => t.Amount <= filter.MaxAmount.Value);

            // Apply pagination
            transactions = transactions.Skip((filter.PageNumber - 1) * filter.PageSize)
                                       .Take(filter.PageSize);

            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new KeyNotFoundException();

            return _mapper.Map<TransactionDto>(transaction);
        }
    }
}
