using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PolicyGenReturnedData
    {
        public string documentNo { get; set; }
        public string policyNo { get; set; }
        public string naicomID { get; set; }
        public string productID { get; set; }
        public string agentID { get; set; }
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
        public string sectionID { get; set; }
        public int sectionSumInsured { get; set; }
        public int sectionPremium { get; set; }
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
