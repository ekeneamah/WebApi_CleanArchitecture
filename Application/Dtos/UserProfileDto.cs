﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserProfileDto
    {
        public UserProfileDto() { }
       

        [Required]
        [MaxLength(50)]
        public required string UserId { get; set; }

       
        [MaxLength(50)]
        public required string Gender { get; set; }
       
        [MaxLength(50)]
        public required DateTime DateofBirth { get; set; }
        [MaxLength(10)]
        public required string MaritalStatus { get; set; }
       
        public required string ResidentialAddress { get; set; }
        
        [MaxLength(120)]
        public required string Town { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; } = null;
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ResidentPerminNo { get; set; }
        public string? Maidenname { get; set; }
        public string? Stateoforigin { get; set; }
        [MaxLength(10),MinLength(10)]
        public string? NIN { get; set; }
        public string? BVN { get; set; }
        public string? BusinessLocation { get; set; }
        public string? SignatureUrl { get; set; }
    }
}
