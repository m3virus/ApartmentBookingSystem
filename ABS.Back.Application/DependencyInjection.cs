using ABS.Back.Application.Abstractions.Behaviors;
using ABS.Back.Domain.Booking;
using ABS.Back.Domain.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ABS.Back.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        service.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        service.AddTransient<PricingService>();

        return service;
    }
}