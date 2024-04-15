using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Models
{
    public class Policy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public required string  UserId { get; set; }
        [Required]
        public required int ProductId {  get; set; }
        [Required]
        public required string ProductCode { get; set; }
        [Required]
        public required string PurchasedDate {  get; set; }
        [Required]
        public required double Price { get; set;}
        [Required]
        public required string TransactionRef { get; set; }
        [Required]
        public required string TransactionStatus { get; set; }
        public required int Coy_Id { get; set; }
        
        public required string PolicyNo { get; set; }
        public required string PaymentRef { get; set; }
    }
}
