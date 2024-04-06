﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDtoDetails
    {
        public string? Product_description { get; set; }

        public int? Product_Id { get; set; }

        public string Product_Name { get; set; }

        public int Coy_Id { get; set; }
        public string Coy_Name { get; set; }
        public int? Category_Id { get; set; }
        public string Category_Name { get; set; }

        public double Product_Price { get; set; }

        public int Product_Quantity { get; set; }

        public string Product_Code { get; set; }
        public object Product_Group { get; set; }
    }

}
