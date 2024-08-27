using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.UnderWriting;

public class ProductUnderWritingDto
{
    [Required]
    public int? ProductId { get; set; }
    
    [Required]
    public List<FormField> Form { get; set; } = null!;
}


public class FormSubmissionDto
{
    [Required]
    public string FormId { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public List<FormAnswerDto> Answers { get; set; } = null!;

    public SubmissionStatus Status { get; set; }   
    
}

public class FormAnswerDto
{
    [Required]
    public string FieldName { get; set; } = null!;

    [Required]
    public string FieldId { get; set; } = null!;

    public List<string> Values { get; set; } = null!;

    public List<IFormFile>? Files { get; set; }   
}




