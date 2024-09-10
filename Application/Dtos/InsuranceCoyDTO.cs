using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos
{
    public class InsuranceCoyDto
    {

        public required string CoyName { get; set; }
        public int CoyId { get; set; }
        public string? CoyDescription { get; set; }
        public string? CoyStatus { get; set; }
        public required string CoyEmail { get; set; }
        public string? CoyCity { get; set; }
        public string? CoyCountry { get; set; }
        public string? CoyPhone { get; set; }
        public string? CoyPostalCode { get; set; }
        public string? CoyState { get; set; }
        public string? CoyStreet { get; set; }
        public string? CoyZipCode { get; set; }
        public string? CoyCityCode { get;set; }
        public string? CoyCountryCode { get; set; }
        public string? CoyVideoLink { get; set; }
        public string? CoyImage { get; set; }
        public string? CoyLogo { get; set; }
        public required bool IsOrg {  get; set; }
        [MaxLength(6)]
        public required string Title { get; set; }
        public required string CoyAgentId { get; set; }    
        public List<CoyBenefitEntity> CoyBenefits { get; set; } = new List<CoyBenefitEntity>();

    }

    public class ListInsuranceCoyDto
    {

        public required string CoyName { get; set; }
        public int CoyId { get; set; }
        public string? CoyDescription { get; set; }
        public string? CoyStatus { get; set; }
        public required string CoyEmail { get; set; }
        public string? CoyCity { get; set; }
        public string? CoyCountry { get; set; }
        public string? CoyPhone { get; set; }
        public string? CoyPostalCode { get; set; }
        public string? CoyState { get; set; }
        public string? CoyStreet { get; set; }
        public string? CoyZipCode { get; set; }
        public string? CoyCityCode { get; set; }
        public string? CoyCountryCode { get; set; }
        public string? CoyVideoLink { get; set; }
        public string? CoyImage { get; set; }
        public string? CoyLogo { get; set; }
        public required bool IsOrg { get; set; }
        [MaxLength(6)]
        public required string Title { get; set; }
        public required string CoyAgentId { get; set; }
        public List<CoyBenefitEntity>? CoyBenefits { get; set; }

    }

    public class InsuranceCoyDetailDto
    {

        public required string CoyName { get; set; }
        public int CoyId { get; set; }
        public string? CoyDescription { get; set; }
        public string? CoyStatus { get; set; }
        public required string CoyEmail { get; set; }
        public string? CoyCity { get; set; }
        public string? CoyCountry { get; set; }
        public string? CoyPhone { get; set; }
        public string? CoyPostalCode { get; set; }
        public string? CoyState { get; set; }
        public string? CoyStreet { get; set; }
        public string? CoyZipCode { get; set; }
        public string? CoyCityCode { get; set; }
        public string? CoyCountryCode { get; set; }
        public string? CoyVideoLink { get; set; }
        public string? CoyImage { get; set; }
        public string? CoyLogo { get; set; }
        public required bool IsOrg { get; set; }
        [MaxLength(6)]
        public required string Title { get; set; }
        public required string CoyAgentId { get; set; }
        public List<CoyBenefitEntity>? CoyBenefits { get; set; }
        public List<ProductForInsuranceCoyDetailsDto>? ProductDetails { get; set; } = new List<ProductForInsuranceCoyDetailsDto>();

    }
}
