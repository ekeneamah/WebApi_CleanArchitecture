using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Application.Dtos
{
    public class InsuredDto
{
    public bool IsOrg { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OtherName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string PhoneLine1 { get; set; }
    public string Address { get; set; }
    public string CityLga { get; set; }
    public string StateId { get; set; }
    public string Nationality { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string TaxIdNumber { get; set; }
    public string KycType { get; set; }
    public string KycNumber { get; set; }
    public DateTime KycIssueDate { get; set; }
    public DateTime KycExpiryDate { get; set; }
}

public class FieldDto
{
       [Key]
        public int Id { get; set; }
        public int SectionId {  get; set; }
    public string Code { get; set; }
    public string Value { get; set; }
}

public class SectionDto
{
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
    public string SectionId { get; set; }
    public int SectionPremium { get; set; }
    public int SectionSumInsured { get; set; }
    public List<FieldDto> Fields { get; set; }
}

public class GetPolicyNumberDto
{
    public string ProductId { get; set; }
    public string AgentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PaymentReferenceId { get; set; }
    public string PaymentAccountId { get; set; }
    public InsuredDto Insured { get; set; }
    public List<SectionDto> Sections { get; set; }
        public string Token { get; set; }
        public string PolicyNo { get; set; }
    }
}