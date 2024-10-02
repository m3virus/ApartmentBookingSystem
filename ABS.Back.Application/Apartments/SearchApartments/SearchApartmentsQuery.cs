using ABS.Back.Application.Abstractions.Messages;

namespace ABS.Back.Application.Apartments.SearchApartments;

public sealed record SearchApartmentsQuery(DateOnly StarDate, DateOnly EndDate):
    IQuery<IReadOnlyList<ApartmentResponse>>;