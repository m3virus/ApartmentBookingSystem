using ABS.Back.Domain.Shared;

namespace ABS.Back.Domain.Booking;

public record PricingDetail(
    Money PricingForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice
    );