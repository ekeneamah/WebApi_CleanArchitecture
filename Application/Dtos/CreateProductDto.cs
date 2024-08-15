using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateProductDto
    {
        public string? ProductName { get; set; }

        public int InsuranceCoyId { get; set; }
        public int? CategoryId { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }


        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductGroup { get; set; }
        public int ProductId { get; set; }
        public InsuranceCoy? InsuranceCoy { get; set; }
        public Category? ProductCategory { get; set; }
        public int CoyId { get; set; }
        public List<ProductBenefit?> Benefit { get; set; } = new List<ProductBenefit?>();

    }

    public class PolicyGenReturnedDataCornerstoneDto
    {
        public static int? ProductId { get; set; }
        public string DocumentNo { get; set; }
        public string PolicyNo { get; set; }
        public string NaicomId { get; set; }
        public string Product_Id { get; set; }
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
        public Section[] Sections { get; set; }
    }

    public class Section
    {
        
        public double SectionSumInsured { get; set; }
        public double SectionPremium { get; set; }
        public Field[] Fields { get; set; }
        public Rate[] Rates { get; set; }
        public Smi[] SmIs { get; set; }
    }

    public class Field
    {
       
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class Rate
    {
        
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class Smi
    {
      
        
        public string Code { get; set; }
        public int SumInsured { get; set; }
        public int Premium { get; set; }
        public int PremiumRate { get; set; }
        public string Description { get; set; }
    }
}
