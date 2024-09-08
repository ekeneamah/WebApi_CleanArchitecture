using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class InsuranceCoy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CoyId { get; set; }


        [Required]
        [MaxLength(100)]
        public string CoyName { get; set; }
        public string? CoyDescription { get; set; }
        [MaxLength(5)]
        public string? CoyStatus { get; set; }
        public required string CoyEmail { get; set; }
        [MaxLength(50)]
        public string? CoyCity { get; set; }
        [MaxLength(50)]
        public string? CoyCountry { get; set; }
        [MaxLength(36)]
        public string? CoyPhone { get; set; }
        [MaxLength(10)]
        public string? CoyPostalCode { get; set; }
        [MaxLength(10)]
        public string? CoyState { get; set; }
        [MaxLength(100)]
        public string? CoyStreet { get; set; }
        [MaxLength(6)]
        public string? CoyZipCode { get; set; }
        [MaxLength(3)]
        public string? CoyCityCode { get; set; }
        [MaxLength(3)]
        public string? CoyCountryCode { get; set; }
        public string? CoyVideoLink { get; set; }
        public string? CoyImage { get; set; }
        public string? CoyLogo { get; set; }
        public required string CoyAgentId { get; set; } = "ABC";
        public bool IsOrg { get; set; }
        public string? Title { get; set; }
        public bool ProvidesQuestionForm { get; set; } = false;

    }

    public class CoyBenefitEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BenefitId { get; set; }

        [Required]
        public int CoyId { get; set; }


        [Required]
        [MaxLength(100)]

        public string? BenefitsTitle { get; set; }
    }
}