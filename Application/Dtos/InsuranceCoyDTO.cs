using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Dtos
{
    public class InsuranceCoyDTO
    {

        public required string Coy_Name { get; set; }
        public int Coy_id { get; set; }
        public string? Coy_Description { get; set; }
        public string? Coy_Status { get; set; }
        public required string Coy_Email { get; set; }
        public string? Coy_City { get; set; }
        public string? Coy_Country { get; set; }
        public string? Coy_Phone { get; set; }
        public string? Coy_PostalCode { get; set; }
        public string? Coy_State { get; set; }
        public string? Coy_Street { get; set; }
        public string? Coy_ZipCode { get; set; }
        public string? Coy_CityCode { get;set; }
        public string? Coy_CountryCode { get; set; }
        public string? Coy_VideoLink { get; set; }
        public string? Coy_Image { get; set; }
        public string? Coy_Logo { get; set; }
        public List<CoyBenefit> Coy_Benefits { get; set; }

    }
}
