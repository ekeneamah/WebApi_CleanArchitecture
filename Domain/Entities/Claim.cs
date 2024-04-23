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
        
        public string LossDate { get; set; }
        public string NotifyDate { get; set; }
       
        [Required]
        public required string Reference { get; set; }
        [Required]
        public  int? InsuranceCompanyId { get; set; }
        public string NotificationNo { get; set; } = string.Empty;
        public string ClaimNo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
