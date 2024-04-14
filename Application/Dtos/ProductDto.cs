using Domain.Models;
using System;
using System.Collections.Generic;
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
        public InsuranceCoyEntity? InsuranceCoy { get; set; }
        public CategoryEntity? Product_Category { get; set; }
        public int Coy_Id { get; set; }
    }
}
