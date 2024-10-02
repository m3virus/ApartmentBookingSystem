using ABS.Back.Application.Abstractions.Clock;
using ABS.Back.Application.Abstractions.Email;
using ABS.Back.Infrastructure.Clock;
using ABS.Back.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABS.Back.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection service,
            IConfiguration configure)
        {
            service.AddTransient<ITimeProvider, DateTimeProvider>();
            service.AddTransient<IEmailService, EmailService>();

            return service;
        }
    }
}
