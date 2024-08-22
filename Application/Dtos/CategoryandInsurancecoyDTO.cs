using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CategoryandInsurancecoyDto
    {
       
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int InsuranceCoyId { get; set; }
        public string InsuranceCoyName { get; set; }
        public InsuranceCoyDto InsuranceCoy { get; set; }
        public CreateCategoryDto CreateCategory { get; set; }
    }
}
