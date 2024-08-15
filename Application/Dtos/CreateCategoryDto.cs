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

        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryDescription { get; set; }
        [MaxLength(450)]
        public string? CategoryVideoLink { get; set; }
        public string? CategoryImage { get; set; }
        public List<CategoryBenefit>? CategoryBenefits { get; set; }
        public required int CategoryId { get; set; }
    }
}
