using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class UnderWritingForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public string Id { get; set; } = null!;
    [JsonIgnore]
    public Product Product { get; set; } = null!;

    [NotMapped]
    public List<FormField> Form { get; set; } = new List<FormField>();

    [JsonIgnore]
    [Required]
    public string FormJson
    {
        get => JsonSerializer.Serialize(Form);
        set => Form = string.IsNullOrEmpty(value) 
            ? new List<FormField>() 
            : JsonSerializer.Deserialize<List<FormField>>(value) ?? new List<FormField>();
    }
}

public class FormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public required string FormId { get; set; }    
    [NotMapped]
    public List<FormAnswer> Answers { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public SubmissionStatus Status { get; set; }   
    [JsonIgnore]
    [Required]

    public string AnswerJson
    {
        get => JsonSerializer.Serialize(Answers);
        set => Answers = string.IsNullOrEmpty(value) 
            ? new List<FormAnswer>() 
            : JsonSerializer.Deserialize<List<FormAnswer>>(value) ?? new List<FormAnswer>();    }
}



public class ClaimsUnderWritingForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public string Id { get; set; } = null!;
    [JsonIgnore]
    public Product Product { get; set; } = null!;
    
    [NotMapped]
    public List<FormField> Form { get; set; } = new List<FormField>();
    [JsonIgnore]
    [Required]

    public string FormJson
    {
        get =>  JsonSerializer.Serialize(Form);
        set => Form = string.IsNullOrEmpty(value) 
            ? new List<FormField>() 
            : JsonSerializer.Deserialize<List<FormField>>(value) ?? new List<FormField>();
        
    }
}

public class ClaimsFormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public required string FormId { get; set; }      
    [NotMapped]
    public List<FormAnswer> Answers { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public SubmissionStatus Status { get; set; }   
    [JsonIgnore]
    [Required]
    public string AnswerJson
    {
        get => JsonSerializer.Serialize(Answers);
        set => Answers = string.IsNullOrEmpty(value) 
            ? new List<FormAnswer>() 
            : JsonSerializer.Deserialize<List<FormAnswer>>(value) ?? new List<FormAnswer>();
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
    public required string FieldId { get; set; }
    [Required]
    public required string FieldName { get; set; }
    public string? DisplayName { get; set; }
    public InputType InputType { get; set; }
    public bool IsRequired { get; set; }
    public List<Option>? Options { get; set; } = new List<Option>();
    public bool AllowsMultiple { get; set; }   
    public string? Placeholder { get; set; }
    public string? RegexValidationPattern { get; set; }
    
    // Properties specific to file upload
    public long? MaxFileSize { get; set; }

    public List<string>? AllowedFileTypes { get; set; } = new List<string>(); // Allowed file extensions (e.g., ".jpg", ".png")
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


