using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByStatusAsync(string status)
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            var filtered = payments.Where(p => p.Status == status)
                                 .OrderByDescending(p => p.PaymentDate);
            return _mapper.Map<IEnumerable<PaymentDto>>(filtered);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            var filtered = payments.Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                                 .OrderByDescending(p => p.PaymentDate);
            return _mapper.Map<IEnumerable<PaymentDto>>(filtered);
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createDto)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.FindAsync(b =>
                    b.BookingId == createDto.BookingId &&
                    (b.PaymentId == null || b.PaymentId == 0));

                if (booking == null)
                {
                    throw new InvalidOperationException("This booking either has its payment or just does not exist.");
                }

                var payment = _mapper.Map<Payment>(createDto);
                payment.PaymentDate = DateTime.UtcNow;
                payment.TotalAmount = booking.PaymentAmount ?? 1000000;
                payment.Currency = "VND";

                if (payment.PaymentMethod == null ||
                    (payment.PaymentMethod.ToLower() != "cash" && payment.PaymentMethod.ToLower() != "vnpay"))
                {
                    payment.PaymentMethod = "Cash";
                }

                if (payment.Status == null ||
                    (payment.Status.ToLower() != "pending" && payment.Status.ToLower() != "paid"))
                {
                    payment.Status = "Pending";
                }

                payment.Tax = payment.TotalAmount * (decimal)0.1;
                payment.Receiver = "Prestine Care";

                await _unitOfWork.Payments.CreateAsync(payment);

                booking.PaymentId = payment.PaymentId;

                if(payment.Status.ToLower() == "paid")
                {
                    await CreateTransactionAsync(payment, booking);
                    booking.PaymentStatus = payment.Status;
                }
                await _unitOfWork.Bookings.UpdateAsync(booking);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<PaymentDto>(payment);
            }
            catch (Exception ex)
            {
                // Log the exception details (ex.Message, ex.StackTrace) for debugging purposes if necessary
                throw new InvalidOperationException("Sorry, but something went wrong.", ex);
            }
        }

        public async Task UpdatePaymentAsync(int id, UpdatePaymentDto updateDto)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            var booking = await _unitOfWork.Bookings.FindAsync(b => b.PaymentId == id);

            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking not found");
            }

            _mapper.Map(updateDto, payment);

            if(payment.Status.ToLower() == "paid")
            {
                await CreateTransactionAsync(payment, booking);
                booking.PaymentStatus = "Paid";
            }

            await _unitOfWork.Payments.UpdateAsync(payment);
            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            await _unitOfWork.Payments.DeleteAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(TransactionFilterDto filter)
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync();

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
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                throw new KeyNotFoundException();

            return _mapper.Map<TransactionDto>(transaction);
        }

        private async Task<Transaction> CreateTransactionAsync(Payment payment, Booking booking)
        {
            var transaction = new Transaction
            {
                PaymentId = payment.PaymentId,
                Amount = payment.TotalAmount,
                Currency = payment.Currency ?? "VND",  
                TransactionType = payment.PaymentMethod ?? "Cash",
                ReferenceNumber = "No reference",
                Sender = payment.Sender ?? "Prestine Care Customer",
                Receiver = payment.Receiver ?? "Prestine Care",
                TransactionDate = DateTime.UtcNow,
                TaxAmount = payment.Tax,
                TaxPercentage = payment.Tax > 0 ? 10m : 0m,
                Description = "Payment for booking services",
                ServiceId = booking.ServiceId,
                ServiceName = booking.ServiceName,
                SourceSystem = "POS System & Mobile App"
            };

            await _unitOfWork.Transactions.CreateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return transaction;
        }

    }
}