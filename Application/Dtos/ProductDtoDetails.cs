using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos
{
    public class ProductDtoDetails
    {
        public string? ProductDescription { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int CoyId { get; set; }
        public string CoyName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductCode { get; set; }
        public object ProductGroup { get; set; }
        public Category ProductCategory { get; set;}
        public InsuranceCoy InsuranceCoy { get; set; }
        public List<ProductBenefit?> Benefit { get; set; } = new List<ProductBenefit?>();

    }

}
