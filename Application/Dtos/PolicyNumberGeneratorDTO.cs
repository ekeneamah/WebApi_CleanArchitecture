using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Application.Dtos
{
    public class Insured
{
    public bool isOrg { get; set; }
    public string title { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string otherName { get; set; }
    public string gender { get; set; }
    public string email { get; set; }
    public string phoneLine1 { get; set; }
    public string address { get; set; }
    public string cityLGA { get; set; }
    public string stateID { get; set; }
    public string nationality { get; set; }
    public DateTime dateOfBirth { get; set; }
    public string taxIdNumber { get; set; }
    public string kycType { get; set; }
    public string kycNumber { get; set; }
    public DateTime kycIssueDate { get; set; }
    public DateTime kycExpiryDate { get; set; }
}

public class Field
{
       [Key]
        public int id { get; set; }
        public int sectionId {  get; set; }
    public string code { get; set; }
    public string value { get; set; }
}

public class Section
{
        [Key]
        public int id { get; set; }
        public int productId { get; set; }
    public string sectionID { get; set; }
    public int sectionPremium { get; set; }
    public int sectionSumInsured { get; set; }
    public List<Field> fields { get; set; }
}

public class GetPolicyNumberDTO
{
    public string productID { get; set; }
    public string agentID { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string paymentReferenceID { get; set; }
    public string paymentAccountID { get; set; }
    public Insured insured { get; set; }
    public List<Section> sections { get; set; }
}
}