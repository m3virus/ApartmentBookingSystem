using ABS.Back.Domain.Abstractions;

namespace ABS.Back.Domain.Booking.Events;

public sealed record BookingCompletedDomainEvent(Guid Id) : IDomainEvent;