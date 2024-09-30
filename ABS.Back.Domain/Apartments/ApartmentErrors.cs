using ABS.Back.Domain.Abstractions;

namespace ABS.Back.Domain.Apartments;

public class ApartmentErrors
{
    public static Error NotFound = new(
        "Apartment.Found",
        "The apartment was not found");
    
}