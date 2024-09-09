using System;
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
       

        public string? UserId { get; set; }

       
        [MaxLength(50)]
        public string? Gender { get; set; }
       
        [MaxLength(50)]
        public  string? DateOfBirth { get; set; }
        [MaxLength(10)]
        public  string? MaritalStatus { get; set; }
       
        public  string? ResidentialAddress { get; set; }
        
        [MaxLength(120)]
        public string? Town { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; } = null;
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ResidentPerminNo { get; set; }
        public string? MaidenName { get; set; }
        public string? StateOfOrigin { get; set; }
        [MaxLength(10),MinLength(10)]
        public string? Nin { get; set; }
        public string? Bvn { get; set; }
        public string? BusinessLocation { get; set; }
        public string? SignatureUrl { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string? Email { get;  set; }
        public string? TaxIdNumber { get; set; }
        public string? LocalGovernment { get; set; }
        public string? OtherName { get; set; }
        public string? UserName { get; set; }
    }
}
