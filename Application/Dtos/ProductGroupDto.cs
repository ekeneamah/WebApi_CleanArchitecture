using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductGroupDto
    {
        public string GroupName {  get; set; }
        public int Count {  get; set; }
        public double AveragePrice {  get; set; }
    }
}
