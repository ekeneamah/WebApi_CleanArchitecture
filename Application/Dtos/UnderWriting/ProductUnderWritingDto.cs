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
    public string Title { get; set; } = null!;
    [Required]
    public List<FormField> GlobalFields { get; set; } = null!;

    public List<FormSection> Sections { get; set; } = new List<FormSection>();

}


public class FormSubmissionDto
{
    [Required]
    public string FormId { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public List<FormAnswerDto> GlobalFieldAnswers { get; set; } = null!;
    
    public List<SectionSubmissionDto>? SectionSubmissions { get; set; }

    public SubmissionStatus Status { get; set; }   
    
}

public class SectionSubmissionDto
{
    public string SectionId { get; set; }
    public string SectionKey { get; set; } 
    // If multiple entries are allowed, store each entry separately
    public List<SectionEntrySubmissionDto> EntrySubmissions { get; set; }
}

public class SectionEntrySubmissionDto
{
    public List<FormAnswerDto> FieldResponses { get; set; }
    public List<SubSectionSubmissionDto> SubSectionSubmissions { get; set; }
}

public class SubSectionSubmissionDto
{
    public string SubSectionId { get; set; }
    public List<FormAnswerDto> FieldResponses { get; set; }
}

public class FormAnswerDto
{
    [Required]
    public string FieldName { get; set; } = null!;

    [Required]
    public string FieldId { get; set; } = null!;

    public List<string> Values { get; set; } = null!;

    // public List<IFormFile>? Files { get; set; }   
}




