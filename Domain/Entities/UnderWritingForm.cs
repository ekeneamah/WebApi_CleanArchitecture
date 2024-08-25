using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class UnderWritingForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public string Id { get; set; } = null!;
    public Product? Product { get; set; }
    
    [NotMapped]
    public List<FormField>? Form { get; set; } = new List<FormField>();

    [JsonIgnore]
    public string? FormJson
    {
        get => Form == null ? null : JsonSerializer.Serialize(Form);
        set => Form = value == null ? null : JsonSerializer.Deserialize<List<FormField>>(value);
    }
}

public class FormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public string? FormId { get; set; }        
    [NotMapped]
    public List<FormAnswer>? Answers { get; set; }  
    public string? UserId { get; set; }             
    public SubmissionStatus Status { get; set; }   
    
    public string? AnswerJson
    {
        get => Answers == null ? null : JsonSerializer.Serialize(Answers);
        set => Answers = value == null ? null : JsonSerializer.Deserialize<List<FormAnswer>>(value);
    }
}



public class ClaimsUnderWritingForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public string Id { get; set; } = null!;
    public Product? Product { get; set; }
    
    [NotMapped]
    public List<FormField>? Form { get; set; } = new List<FormField>();

    public string? FormJson
    {
        get => Form == null ? null : JsonSerializer.Serialize(Form);
        set => Form = value == null ? null : JsonSerializer.Deserialize<List<FormField>>(value);
    }
}

public class ClaimsFormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public string? FormId { get; set; }        
    [NotMapped]
    public List<FormAnswer>? Answers { get; set; }  
    public string? UserId { get; set; }             
    public SubmissionStatus Status { get; set; }   
    
    public string? AnswerJsonString
    {
        get => Answers == null ? null : JsonSerializer.Serialize(Answers);
        set => Answers = value == null ? null : JsonSerializer.Deserialize<List<FormAnswer>>(value);
    }
}

public class Option
{
    [Required]
    public string Key { get; set; }
    [Required]

    public string Value { get; set; }

    public Option(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
public class FormField
{
    [Required]
    public string? FieldId { get; set; }
    [Required]
    public string? FieldName { get; set; }
    public string? DisplayName { get; set; }
    public InputType InputType { get; set; }
    public bool IsRequired { get; set; }
    public List<Option>? Options { get; set; } 
    public bool AllowsMultiple { get; set; }   
    public string? Placeholder { get; set; }
    public string? RegexValidationPattern { get; set; }
    
    // Properties specific to file upload
    public long? MaxFileSize { get; set; }         
    public List<string>? AllowedFileTypes { get; set; } // Allowed file extensions (e.g., ".jpg", ".png")
}


public class FormAnswer
{
    [Required]
    public string FieldName { get; set; } = null!;

    [Required]
    public string FieldId { get; set; } = null!;

    [Required]
    public List<string> Values { get; set; } = null!;

    public List<string>? FileUrls { get; set; }   
}




public enum SubmissionStatus
{
    Draft,
    Submitted
}

public enum InputType
{
    Text,
    Number,
    Select,
    Radio,
    Checkbox,
    Password,
    Email,
    Date,
    Upload  
}


public class FormAnswerDto
{
    public string? FieldId { get; set; }
    public string? FieldName { get; set; }        
    public List<string>? Values { get; set; }     
    public List<IFormFile>? Files { get; set; }   
}
