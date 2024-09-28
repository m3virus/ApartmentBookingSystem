namespace ABS.Back.Domain.Apartments;

public record Address(string Country,
    string State,
    string City,
    string Street,
    string ZipCode
);