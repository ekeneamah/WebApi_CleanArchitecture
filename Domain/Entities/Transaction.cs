﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Transaction
    {
        public string? Authorization_Url { get; set; }
        public string? AccessCode { get; set; }
        [Key]
        public required string Reference { get; set; }
        public string? Status { get; set; } = "Open";
        public string? UserId { get; set; }
        public int? ProductId { get; set; }
        public string? PolicyNo { get; set; }
        public double Amount { get; set; }
        public DateTime? DateTime { get; set; }
        public string? UserEmail { get; set; }
        public string? PaymentRef { get; set; }
    }

    public class TransactionDTO
    {
        public string? Authorization_Url { get; set; }
        public string? Access_Code { get; set; }
        [Key]
        public required string Reference { get; set; }
        public string? Status { get; set; } = "Open";
        public string? UserId { get; set; }
        public int? ProductId { get; set; }
        public string? PolicyNo { get; set; }
        public double Amount { get; set; }
        public DateTime? DateTime { get; set; }
        public string? UserEmail { get; set; }
        public string? PaymentRef { get; set; }
        public  Product? Product{get;set;}
    }
}
