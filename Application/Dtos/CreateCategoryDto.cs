using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos
{
    public class CreateCategoryDto
    {

        [Required]
        [MaxLength(100)]

        public string CategoryName { get; set; } = null!;
        [Required]
        public string? CategoryDescription { get; set; }
        [MaxLength(450)]
        public string? CategoryVideoLink { get; set; }
        public string? CategoryImage { get; set; }
        public List<CategoryBenefit>? CategoryBenefits { get; set; } = new List<CategoryBenefit>();
        public required int CategoryId { get; set; }
    }
    
    public class CategoryBenefitDto
    {
        
        [Required]
        [MaxLength(250)]

        public string? BenefitsTitle { get; set; }

        [Required]
        public required int BenefitCategoryId { get; set;}
    }

}
