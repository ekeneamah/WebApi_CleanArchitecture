﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Categoty_Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string? Category_Name { get; set; }
        [Required]
        [MaxLength(450)]
        public string? Category_Description { get; set; }
        [MaxLength(450)]
        public string? Category_VideoLink { get; set; }



    }
}
