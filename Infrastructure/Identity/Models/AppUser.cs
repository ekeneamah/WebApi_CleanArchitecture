using Application.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(256)]
        public string OTP { get; set; }

        [Required]
        [StringLength(256)]
        public DateTime OtpTimestamp { get; set; }

        public bool IsActivated { get;  set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
        
    }
}