using ABS.Back.Application.Abstractions.Messages;

namespace ABS.Back.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(Guid UserId, Guid ApartmentId, DateOnly Start, DateOnly End):ICommand<Guid>;