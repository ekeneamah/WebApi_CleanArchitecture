using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CategoryDTO
    {

        [Required]
        [MaxLength(100)]

        public string? Category_Name { get; set; }
        [Required]
        public string? Category_Description { get; set; }
        [MaxLength(450)]
        public string? Category_VideoLink { get; set; }
        public string? Category_Image { get; set; }
        public List<CategoryBenefit>? Category_Benefits { get; set; }
        public required int category_id { get; set; }
    }
}
