using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string? CoyProductId { get; set; }


        [Required]
        [MaxLength(100)]
        public string? ProductName { get; set; }


        [ForeignKey("InsuranceCompany")]
        public int CoyId { get; set; }
       

        [ForeignKey("CategoryEntity")]
        public int? CategoryId { get; set; }
       



        [Required]
        [MaxLength(100)]
        public double ProductPrice { get; set; }
        public double ProductPricePercentatage { get; set; }

        public int ProductQuantity { get; set; }

        public string? ProductCode { get; set; }
        public string? ProductGroup { get; set; }
        public string? ProductDescription { get; set; }
        public bool? IsApproved { get; set;}
        public bool? IsDeleted { get; set;}
        public bool IsRecommended { get; set; } = false;
        public int? SortingWeight { get; set; }
        public PriceType PriceType { get; set; }
        public string? ThumbNail { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool RequireInspection { get; set; }
        public List<ProductBenefit?> Benefit { get; set; } = new List<ProductBenefit?>();

    }
    
    
    public enum PriceType
    {
        Fixed,
        Percentage
    }


}
