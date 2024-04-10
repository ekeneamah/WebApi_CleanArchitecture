using Application.Dtos.Account;
using Application.Dtos.Email;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
        Task SendEmailAsync2(string v1, string v2, string v3);
    }
}