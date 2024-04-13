using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class kycEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? IdentityType { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public required string IdentityNumber { get; set; }
        [Required, NotNull]
        public DateTime FromExpiryDate { get; set; }
        [Required]
        public DateTime ToExpiryDate { get;set; }
        [Required]
        [ForeignKey("User")] // Specify the foreign key constraint
        public string? UserId { get; set; }

       
    }
}
