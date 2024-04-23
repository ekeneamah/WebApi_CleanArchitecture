using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string? Product_Name { get; set; }


        [ForeignKey("InsuranceCompany")]
        public int Coy_Id { get; set; }
       

        [ForeignKey("CategoryEntity")]
        public int? Category_Id { get; set; }
       



        [Required]
        [MaxLength(100)]
        public double Product_Price { get; set; }

        public int Product_Quantity { get; set; }

        public string? Product_Code { get; set; }
        public string? Product_Group { get; set; }
        public string? Product_Description { get; set; }
        public bool? Is_Approved { get; set;}
        public bool? Is_Deleted { get; set;}
        public bool Is_Recommended { get; set; } = false;
        public int? SortingWeight { get; set; }
    }


}
