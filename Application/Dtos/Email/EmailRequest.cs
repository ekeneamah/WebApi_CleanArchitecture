using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Email
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string CCEmail { get; set; } = string.Empty;
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}