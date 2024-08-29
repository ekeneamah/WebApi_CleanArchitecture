using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PolicyGenReturnedDataCornerstone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DocumentNo { get; set; }
        public string PolicyNo { get; set; }
        public string NaicomId { get; set; }
        public string ProductId { get; set; }
        public string AgentId { get; set; }
        public string Certificate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SumInsured { get; set; }
        public int Premium { get; set; }
        public int FxRate { get; set; }
        public string FxCurrency { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        
    }

    public class PolicySection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PolicyGenReturnedDataCornerstoneId { get; set; }
        public double SectionSumInsured { get; set; }
        public double SectionPremium { get; set; }
       
    }

    public class PolicySectionField
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required int PolicySectionId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class PolicySectionRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required int PolicySectionId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class PolicySectionSmi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required int PolicySectionId { get; set; }
        public string Code { get; set; }
        public int SumInsured { get; set; }
        public int Premium { get; set; }
        public int PremiumRate { get; set; }
        public string Description { get; set; }
    }
}
