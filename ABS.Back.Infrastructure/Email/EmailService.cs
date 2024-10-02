using ABS.Back.Application.Abstractions.Email;

namespace ABS.Back.Infrastructure.Email
{
    internal sealed class EmailService:IEmailService
    {
        public Task SendEmailAsync(Domain.Users.Email Email, string Subject, string Body)
        {
            return Task.CompletedTask;

        }
    }
}
