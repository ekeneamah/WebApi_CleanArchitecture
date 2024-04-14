using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Categoty_Id { get; set; }


        [Required]
        [MaxLength(100)]
        
        public string? Category_Name { get; set; }
        [Required]
        public string? Category_Description { get; set; }
        [MaxLength(450)]
        public string? Category_VideoLink { get; set; }
        public string? Category_Image {  get; set; }
        public List<CategoryBenefitEntity>? Category_Benefits { get; set; }



    }
    public class CategoryBenefitEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Benefit_Id { get; set; }


        [Required]
        [MaxLength(100)]

        public string? Benefits_Title { get; set; }

        [Required]
        public required int Category_id { get; set;}
    }

    }
