using ABS.Back.Application.Abstractions.Messages;

namespace ABS.Back.Application.Bookings.GetBooking;

public record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>
{

}