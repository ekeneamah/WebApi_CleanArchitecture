using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class InsuranceCoyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Coy_Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Coy_Name { get; set; }
        public string? Coy_Description { get; set; }
        [MaxLength(5)]
        public string? Coy_Status { get; set; }
        public required string Coy_Email { get; set; }
        [MaxLength(50)]
        public string? Coy_City { get; set; }
        [MaxLength(50)]
        public string? Coy_Country { get; set; }
        [MaxLength(36)]
        public string? Coy_Phone { get; set; }
        [MaxLength(10)]
        public string? Coy_PostalCode { get; set; }
        [MaxLength(10)]
        public string? Coy_State { get; set; }
        [MaxLength(100)]
        public string? Coy_Street { get; set; }
        [MaxLength(6)]
        public string? Coy_ZipCode { get; set; }
        [MaxLength(3)]
        public string? Coy_CityCode { get; set; }
        [MaxLength(3)]
        public string? Coy_CountryCode { get; set; }
        public string? Coy_VideoLink { get; set; }
        public string? Coy_Image { get; set; }
        public string? Coy_Logo { get; set; }
        public required string Coy_AgentId { get; set; } = "ABC";
        public bool IsOrg { get; set; }
        public string Title { get; set; }
    }

    public class CoyBenefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Benefit_Id { get; set; }

        [Required]
        public int Coy_id { get; set; }


        [Required]
        [MaxLength(100)]

        public string? Benefits_Title { get; set; }
    }
}