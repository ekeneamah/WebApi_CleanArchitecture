using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class TransactionDTO
    {
        

        public required string UserId { get; set; }
        [Required]
        public required int? ProductId { get; set; }
        [Required]
        public required string ProductCode { get; set; }
        [Required]
        public required DateTime PurchasedDate { get; set; }
        [Required]
        public required decimal Price { get; set; }
        [Required]
        public required string TransactionRef { get; set; }
        [Required]
        public required string TransactionStatus { get; set; }
        public required int Coy_Id { get; set; }
       
        public int PolicyId { get; set; }
        public string PolicyNo { get; set; }
        public string PaymentRef { get; set; }
    }
}
