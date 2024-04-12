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

        [StringLength(256)]
        public string? OTP { get; set; }

        [StringLength(256)]
        public DateTime OtpTimestamp { get; set; }

        public bool IsActivated { get;  set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

       


      
        [MaxLength(50)]
        public string? DateofBirth { get; set; }
        [MaxLength(10)]
        public string? MaritalStatus { get; set; }
        public string? ResidentialAddress { get; set; }
       
        [MaxLength(120)]
        public string? Town { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; } = null;
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ResidentPerminNo { get; set; }
        public string? Maidenname { get; set; }
        public string? Stateoforigin { get; set; }
        public string? NIN { get; set; }
        public string? BVN { get; set; }
        public string? BusinessLocation { get; set; }
        public string? SignatureUrl { get; set; }
        public string? Gender { get; set; }

    }
}