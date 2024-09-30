using ABS.Back.Domain.Booking;
using Microsoft.Extensions.DependencyInjection;

namespace ABS.Back.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        service.AddTransient<PricingService>();

        return service;
    }
}