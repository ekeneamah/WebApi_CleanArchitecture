using Application.Dtos.Account;
using Application.Dtos.Email;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
        Task SendEmailAsync2(string toEmail, string subject, string body);
    }
}