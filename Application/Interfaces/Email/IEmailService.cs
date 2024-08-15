using Application.Dtos.Email;

namespace Application.Interfaces.Email
{
    public interface IEmailService
    {
       // Task SendEmailAsync(EmailRequest request);
        Task SendEmailAsync(EmailRequest request);
    }
}