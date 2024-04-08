﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class InsuranceCoy
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
    }
}