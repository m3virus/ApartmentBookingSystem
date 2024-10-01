using ABS.Back.Domain.Apartments;

namespace ABS.Back.Domain.Booking;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellation);
    void Add(Booking booking);
}