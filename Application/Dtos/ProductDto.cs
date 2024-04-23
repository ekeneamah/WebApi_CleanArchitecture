using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDto
    {
        public string? Product_Name { get; set; }

        public int InsuranceCoy_id { get; set; }
        public int? Category_Id { get; set; }

        public double Product_Price { get; set; }

        public int Product_Quantity { get; set; }


        public string Product_Code { get; set; }
        public string Product_Description { get; set; }
        public string Product_Group { get; set; }
        public int Product_Id { get; set; }
        public InsuranceCoy? InsuranceCoy { get; set; }
        public Category? Product_Category { get; set; }
        public int Coy_Id { get; set; }
    }

    public class PolicyGenReturnedData_cornerstoneDTO
    {
        public static int? ProductId { get; set; }
        public string documentNo { get; set; }
        public string policyNo { get; set; }
        public string naicomID { get; set; }
        public string productID { get; set; }
        public string agentID { get; set; }
        public string Certificate { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int sumInsured { get; set; }
        public int premium { get; set; }
        public int fxRate { get; set; }
        public string fxCurrency { get; set; }
        public string customerName { get; set; }
        public string customerID { get; set; }
        public Section[] sections { get; set; }
    }

    public class Section
    {
        
        public double sectionSumInsured { get; set; }
        public double sectionPremium { get; set; }
        public Field[] fields { get; set; }
        public Rate[] rates { get; set; }
        public Smi[] smIs { get; set; }
    }

    public class Field
    {
       
        public string code { get; set; }
        public string value { get; set; }
    }

    public class Rate
    {
        
        public string code { get; set; }
        public string value { get; set; }
    }

    public class Smi
    {
      
        
        public string code { get; set; }
        public int sumInsured { get; set; }
        public int premium { get; set; }
        public int premiumRate { get; set; }
        public string description { get; set; }
    }
}
