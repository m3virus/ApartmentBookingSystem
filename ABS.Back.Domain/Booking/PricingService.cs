using ABS.Back.Domain.Apartments;
using ABS.Back.Domain.Shared;

namespace ABS.Back.Domain.Booking
{
    public class PricingService
    {
        public PricingDetail Calculate(Apartment apartment, DateRange duration)
        {
            var currency = apartment.Price.Currency;

            var priceForPeriod = new Money(apartment.Price.Amount * duration.CreateDuration, currency);

            decimal upChargePercentage = 0;

            foreach (var Counter in apartment.Amenities)
            {
                upChargePercentage += Counter switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.03m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }

            var amenityUpCharge = Money.ZeroMoney(currency);
            if (upChargePercentage > 0)
            {
                amenityUpCharge = new Money(priceForPeriod.Amount * upChargePercentage,
                    currency);

            }

            var totalPrice = Money.ZeroMoney();
            totalPrice += priceForPeriod;
            if (!apartment.CleaningFee.IsZero())
            {
                totalPrice += apartment.CleaningFee;
            }

            totalPrice += amenityUpCharge;

            return new PricingDetail(priceForPeriod, apartment.CleaningFee, amenityUpCharge, totalPrice);
        }
    }
}
