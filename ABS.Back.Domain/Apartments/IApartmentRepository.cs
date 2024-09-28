namespace ABS.Back.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment> GetByIdAsync(Guid id);
}