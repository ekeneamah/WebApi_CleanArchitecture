using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }


        [Required]
        [MaxLength(100)]
        
        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryDescription { get; set; }
        [MaxLength(450)]
        public string? CategoryVideoLink { get; set; }
        public string? CategoryImage {  get; set; }

        public bool IsActive { get; set; } = true;
        

    }
    public class CategoryBenefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BenefitId { get; set; }


        [Required]
        [MaxLength(250)]

        public string? BenefitsTitle { get; set; }

        [Required]
        public required int BenefitCategoryId { get; set;}
    }

    public class ProductBenefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BenefitId { get; set; }


        [Required]
        [MaxLength(250)]

        public string? BenefitsTitle { get; set; }

        [Required]
        [MaxLength(250)]

        public required string BenefitProductId { get; set;}
    }

    }
