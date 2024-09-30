using ABS.Back.Domain.Apartments;

namespace ABS.Back.Domain.Booking;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);

    void Add(Booking booking);
}