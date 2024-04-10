using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "ekene.amahxs.ea@gmail.com";

        [Required]
        public string Password { get; set; } = "5212@Abc";
    }
}