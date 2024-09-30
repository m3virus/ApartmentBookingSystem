using ABS.Back.Application.Abstractions.Clock;
using ABS.Back.Application.Abstractions.Messages;
using ABS.Back.Domain.Abstractions;
using ABS.Back.Domain.Apartments;
using ABS.Back.Domain.Booking;
using ABS.Back.Domain.Users;

namespace ABS.Back.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler:ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly ITimeProvider _timeProvider;

    public ReserveBookingCommandHandler(
        PricingService pricingService,
        IBookingRepository bookingRepository, 
        IApartmentRepository apartmentRepository, 
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        ITimeProvider timeProvider)
    {
        _pricingService = pricingService;
        _bookingRepository = bookingRepository;
        _apartmentRepository = apartmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var duration = DateRange.Create(request.Start, request.End);

        if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        var booking = Booking.ReserveFactory(apartment, user.Id, duration, _timeProvider.UtcNow, _pricingService);

        _bookingRepository.Add(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id;
    }
}