using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Policy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [Required]
        public string? ProductCode { get; set; }

        [Required]
        public DateTime PurchasedDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string? TransactionRef { get; set; }

        [Required]
        public string? TransactionStatus { get; set; }

        public int CoyId { get; set; }

        [Required]
        public string? PolicyNo { get; set; }

        [Required]
        public string? PaymentRef { get; set; }
    }
}
