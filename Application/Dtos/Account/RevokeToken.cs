using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account
{
    public class RevokeToken
    {
        [Required]
        public string? Token { get; set; }
    }
}