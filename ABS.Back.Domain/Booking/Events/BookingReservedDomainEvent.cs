using ABS.Back.Domain.Abstractions;

namespace ABS.Back.Domain.Booking.Events;

public sealed record BookingReservedDomainEvent(Guid Id, Guid BookingId) : IDomainEvent
{
    
}