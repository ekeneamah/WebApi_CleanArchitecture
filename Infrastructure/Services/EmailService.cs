using Application.Dtos.Email;
using Application.Interfaces;
using Domain.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using Application.Dtos.Account;
using Azure;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Net.Mail;
//using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }
        private  System.Net.Mail.SmtpClient _smtpClient;
        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                //email.Cc.UpdateUser(MailboxAddress.Parse(request.CCEmail));
                email.From.Add(MailboxAddress.Parse("developers@transparencyscape.onmicrosoft.com"));
                email.Subject =request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                var e = email;
                await smtp.SendAsync(e);
                smtp.Disconnect(true);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }


        public async Task SendEmailAsync2(string toEmail, string subject, string body)
        {
            _smtpClient = new System.Net.Mail.SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("developers@transparencyscape.onmicrosoft.com", "Gaq984382")
            };
            try
            {
                var message = new MailMessage("developers@transparencyscape.onmicrosoft.com", toEmail, subject, body)
                {
                    IsBodyHtml = true
                };

                await _smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Handle exceptions
               _logger.LogError($"Failed to send email: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }





    }
}