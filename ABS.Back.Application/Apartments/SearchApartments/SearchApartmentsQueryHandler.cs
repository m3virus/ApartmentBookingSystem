using ABS.Back.Application.Abstractions.Data;
using ABS.Back.Application.Abstractions.Messages;
using ABS.Back.Domain.Abstractions;
using ABS.Back.Domain.Booking;
using Dapper;

namespace ABS.Back.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentsQueryHandler(ISqlConnectionFactory connectionFactory) :
    IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed
    };
    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.CreateConnection();

        const string sql = """

                           Select
                           a.id as Id,
                           a.name as Name,
                           a.price_amount as Price,
                           a.price_currency as Currency,
                           a.address_country as Country,
                           a.address_state as State,
                           a.address_zip_code as ZipCode,
                           a.address_city as City,
                           a.address_street as Street
                           From apartments as a 
                           where not exists
                           (
                           select 1
                           from bookings as b
                           where 
                           b.apartments_id = a.id and 
                           b.duration_start <= @EndDate and 
                           b.duration_end >= @StartDate and 
                           b.status = ANY(@ActiveBookingStatuses)
                           )

                           """;
        var apartments = await connection.QueryAsync<ApartmentResponse,
            AddressResponse,
            ApartmentResponse>(
            sql, (apartment, address) =>
            {
                apartment.Address = address;
                return apartment;
            },
            new
            {
                request.StarDate,
                request.EndDate,
                ActiveBookingStatuses
            },
            splitOn: "Country");

        return apartments.ToList();
    }
}