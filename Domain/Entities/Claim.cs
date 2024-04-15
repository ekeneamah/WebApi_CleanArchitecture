using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Claim
    {
        
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public  Guid ClaimsId { get; set; } // Guid for PolicyEntity ID
        [Required]
        [MaxLength(100)]
        public required string UserId { get; set; } // User ID
        [Required]
        public required string PolicyNo { get; set; }
        
        public DateTime LossDate { get; set; }
        public DateTime NotifyDate { get; set; }
        [Required]
        public required string ClaimForm { get; set; }
        [Required]
        public required string Reference { get; set; }
        [Required]
        public required int InsuranceCompanyId { get; set; }
    }
}
