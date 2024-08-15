using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserProfile
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UserId { get; set; }

      
        [Required]
        [MaxLength(50)]
        public required string DateofBirth { get; set; }
        [Required, MaxLength(10)]
        public required string MaritalStatus { get; set; }
        [Required]
        public required string ResidentialAddress { get; set; }
        [Required]
        [MaxLength(120)]
        public required string Town { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; } = null;
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ResidentPerminNo { get; set; }
        public string? Maidenname { get; set; }
        public string? Stateoforigin { get; set; }
       
        public string? Bvn { get; set; }
        public string? BusinessLocation { get; set; }
        public string? SignatureUrl { get; set; }
        public string? Gender { get; set;}
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Nin { get; set; }
    }
}
