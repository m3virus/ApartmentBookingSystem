using ABS.Back.Application.Abstractions.Email;
using ABS.Back.Domain.Booking;
using ABS.Back.Domain.Booking.Events;
using ABS.Back.Domain.Users;
using MediatR;

namespace ABS.Back.Application.Bookings.ReserveBooking;

public class BookingReserveDomainEventHandler(
    IEmailService emailService,
    IUserRepository userRepository,
    IBookingRepository bookingRepository) 
    : INotificationHandler<BookingReservedDomainEvent>
{
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null)
        {
            return;
        }

        var user = await userRepository.GetUserByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await emailService.SendEmailAsync(
            user.Email,
            "Booking Apartment",
            "You have 10 minutes to confirm this reservation.");

    }
}