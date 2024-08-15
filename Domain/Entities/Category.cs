﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_Id { get; set; }


        [Required]
        [MaxLength(100)]
        
        public string? Category_Name { get; set; }
        [Required]
        public string? Category_Description { get; set; }
        [MaxLength(450)]
        public string? Category_VideoLink { get; set; }
        public string? Category_Image {  get; set; }
     



    }
    public class CategoryBenefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Benefit_Id { get; set; }


        [Required]
        [MaxLength(250)]

        public string? Benefits_Title { get; set; }

        [Required]
        public required int Benefit_Category_id { get; set;}
    }

    }
