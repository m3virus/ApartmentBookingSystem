using ABS.Back.Application.Abstractions.Email;
using ABS.Back.Domain.Booking;
using ABS.Back.Domain.Booking.Events;
using ABS.Back.Domain.Users;
using MediatR;

namespace ABS.Back.Application.Bookings.ReserveBooking;

public class BookingReserveDomainEventHandler :INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingReserveDomainEventHandler(IEmailService emailService, IUserRepository userRepository, IBookingRepository bookingRepository)
    {
        _emailService = emailService;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null)
        {
            return;
        }

        var user = await _userRepository.GetUserByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendEmailAsync(
            user.Email,
            "Booking Apartment",
            "You have 10 minutes to confirm this reservation.");

    }
}