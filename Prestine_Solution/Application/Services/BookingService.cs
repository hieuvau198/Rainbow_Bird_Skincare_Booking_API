using Application.DTOs;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(null, b => b.Customer);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id, b => b.Customer);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(b => b.CustomerId == customerId, b => b.Customer);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByTherapistIdAsync(int therapistId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(b => b.TherapistId == therapistId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByServiceIdAsync(int serviceId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(b => b.ServiceId == serviceId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByDateAsync(DateOnly date)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(b => b.BookingDate == date);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderBy(b => b.SlotId));
        }

        public async Task<GetBookingStatusDto> GetBookingStatusAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            var nextStatuses = BookingStatusHelper.GetNextStatuses(booking.Status);

            return new GetBookingStatusDto
            {
                CurrentStatus = booking.Status, // ✅ Return the already stored display name
                NextStatuses = nextStatuses
            };
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createDto)
        {
            var existingBookings = await _unitOfWork.Bookings.GetAllAsync(b =>
                b.CustomerId == createDto.CustomerId &&
                b.BookingDate == createDto.BookingDate &&
                b.SlotId == createDto.SlotId
            );

            if (existingBookings.Any())
                throw new InvalidOperationException("You already have a booking for this time slot. Please choose a different time.");

            Service service = await _unitOfWork.Services.FindAsync(s => s.ServiceId == createDto.ServiceId);
            if (service == null)
                throw new InvalidOperationException("This service is no longer existed.");

            string therapistName = "No Therapist Assigned";
            if (createDto.TherapistId != 0)
            {
                Therapist therapist = await _unitOfWork.Therapists.FindAsync(t => t.TherapistId == createDto.TherapistId, t => t.User);
                if (therapist != null)
                {
                    therapistName = therapist.User.FullName;
                }
            }

            service.BookingNumber++;

            await _unitOfWork.Services.UpdateAsync(service);

            var booking = _mapper.Map<Booking>(createDto);
            booking.CreatedAt = DateTime.UtcNow;
            booking.Status = BookingStatusHelper.GetStatusDisplayName(BookingStatus.AwaitingConfirmation); // ✅ Store as "Awaiting Confirmation"
            booking.ServiceName = service.ServiceName ?? "Service Name Not Found";
            booking.Currency = service.Currency ?? "VND";
            booking.DurationMinutes = service.DurationMinutes;
            booking.PaymentAmount = booking.ServicePrice + booking.BookingFee;
            booking.Location = "Prestine Care Center, District 9";
            booking.IsRated = false;
            booking.IsFeedback = false;
            booking.PaymentStatus = PaymentStatus.Pending.ToString();
            booking.TherapistName = therapistName;

            await _unitOfWork.Bookings.CreateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task UpdateBookingAsync(int id, UpdateBookingDto updateDto)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("Sorry! This booking does not exist.");

            if (updateDto.TherapistId == 0 && updateDto.Status.Equals("Confirmed"))
            {
                throw new InvalidOperationException("Assign a therapist for this booking before confirm it.");
            }
            if(updateDto.TherapistId != booking.TherapistId)
            {
                await UpdateBookingTherapistAsync(id, updateDto.TherapistId);
            }
            _mapper.Map(updateDto, booking);
            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBookingStatusAsync(int id, string newStatusString)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            BookingStatus? newStatus = BookingStatusHelper.ParseBookingStatus(newStatusString);
            if (newStatus == null)
                throw new ArgumentException($"The status '{newStatusString}' is not valid. Please try again.");

            BookingStatus currentStatus = BookingStatusHelper.ParseBookingStatus(booking.Status)
                ?? throw new InvalidOperationException("The booking has an invalid status. Please contact support.");

            var allowedStatuses = BookingStatusHelper.GetNextStatuses(currentStatus);
            if (!allowedStatuses.Contains(newStatus.Value))
                throw new InvalidOperationException($"You cannot change the status from '{booking.Status}' to '{newStatusString}'. Please check and try again.");

            booking.Status = BookingStatusHelper.GetStatusDisplayName(newStatus.Value); // ✅ Store as display name

            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBookingStatusAsync(int id, UpdateBookingDto updateDto)
        {
            if (updateDto.Status == null)
                throw new ArgumentException("Status cannot be null.");

            await UpdateBookingStatusAsync(id, updateDto.Status);
        }

        public async Task UpdateBookingTherapistAsync(int bookingId, int newTherapistId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            var therapist = await _unitOfWork.Therapists.FindAsync(t => t.TherapistId == newTherapistId, t => t.User);
            if (therapist == null)
                throw new KeyNotFoundException("The specified therapist does not exist.");

            var isTherapistAvailable = await _unitOfWork.TherapistAvailabilities.ExistsAsync(a =>
                a.TherapistId == newTherapistId &&
                a.SlotId == booking.SlotId &&
                a.Status == "Available" // Ensure the status indicates the therapist is working
            );

            if (!isTherapistAvailable)
                throw new InvalidOperationException("The selected therapist is not available at this time. Please choose a different therapist.");

            var isTherapistBooked = await _unitOfWork.Bookings.ExistsAsync(b =>
                b.TherapistId == newTherapistId &&
                b.BookingDate == booking.BookingDate &&
                b.SlotId == booking.SlotId &&
                b.BookingId != bookingId // Ensure we are not checking against the same booking
            );

            if (isTherapistBooked)
                throw new InvalidOperationException("The selected therapist is already booked at this time slot. Please choose a different therapist.");

            booking.TherapistId = newTherapistId;
            booking.TherapistName = therapist.User.FullName;

            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            await _unitOfWork.Bookings.DeleteAsync(booking);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}