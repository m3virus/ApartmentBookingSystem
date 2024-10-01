namespace ABS.Back.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(Domain.Users.Email Email, string Subject, string Body);
}