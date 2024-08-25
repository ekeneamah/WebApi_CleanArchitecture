using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace Application.Dtos.UnderWriting;

public class ProductUnderWritingDto
{
    [Required]
    public int? ProductId { get; set; }
    
    [Required]
    public List<FormField> Form { get; set; } = null!;
}


