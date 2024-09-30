using System.Runtime.CompilerServices;
using ABS.Back.Domain.Abstractions;
using ABS.Back.Domain.Apartments;
using ABS.Back.Domain.Booking.Events;
using ABS.Back.Domain.Shared;

namespace ABS.Back.Domain.Booking;

public sealed class Booking : Entity
{
    private Booking(Guid id,
        Guid apartmentId,
        Guid userId,
        DateRange duration, 
        Money priceForPerion, 
        Money cleaningFee, 
        Money amenitiesUpCharge, 
        Money totalPrice,
        DateTime createOnUtc) : 
        base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPerion = priceForPerion;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        CreateOnUtc = createOnUtc;
    }

    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public DateRange Duration { get; private set; }
    public Money PriceForPerion { get; private set; }
    public Money CleaningFee { get; private set; }
    public Money AmenitiesUpCharge { get; private set; }
    public Money TotalPrice { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTime CreateOnUtc { get; private set; }
    public DateTime? ConfirmedOnUtc { get; private set; }
    public DateTime? CancelledOnUtc { get; private set; }
    public DateTime? RejectedOnUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }

    public static Booking ReserveFactory(
        Apartment apartment
        , Guid userId
        , DateRange duration
        , DateTime createOnUtc
        , PricingService service)
    {
        var pricingDetail = service.Calculate(apartment, duration);
        var booking = new Booking(
            Guid.NewGuid(),
            apartment.Id,
            userId,
            duration,
            pricingDetail.PricingForPeriod,
            pricingDetail.CleaningFee,
            pricingDetail.AmenitiesUpCharge,
            pricingDetail.TotalPrice,
            createOnUtc
        );
        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

        apartment.LastBookOnUtc = DateTime.UtcNow;

        return booking; 
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow; 
        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Rejected;
        ConfirmedOnUtc = utcNow;
        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        Status = BookingStatus.Completed;
        ConfirmedOnUtc = utcNow;
        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        var currentday = DateOnly.FromDateTime(utcNow);

        if (currentday > Duration.Start)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }
        Status = BookingStatus.Cancelled;
        ConfirmedOnUtc = utcNow;
        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }


}



