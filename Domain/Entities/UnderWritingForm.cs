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
    [Required]
    [NotMapped]
    public string Title { get; set; } = null!;

    [JsonIgnore]
    public Product Product { get; set; } = null!;

    [NotMapped]
    [JsonIgnore]
    public List<FormField> GlobalFields { get; set; } = new List<FormField>(); // Questions that don't belong to a section.
    
    [NotMapped]
    [JsonIgnore]
    public List<FormSection>? Sections { get; set; }

    [Required]
    public string FormJson
    {
        get => JsonSerializer.Serialize(new { GlobalFields, Sections }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase,     Converters = { new JsonStringEnumConverter() }
        });
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var deserializedForm = JsonSerializer.Deserialize<FormBody>(value);
                if (deserializedForm != null)
                {
                    GlobalFields = deserializedForm.Form ?? new List<FormField>();
                    Sections = deserializedForm.Sections;
                }
            }
            else
            {
                GlobalFields = new List<FormField>();
                Sections = new List<FormSection>();
            }
        }
    }
}

// Body structure to deserialize the form JSON
public class FormBody
{
    public List<FormField> Form { get; set; } = new List<FormField>(); // Questions that don't belong to a section.
    public List<FormSection>? Sections { get; set; }
}

// Model for form fields
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
    public string? OptionsApiEndpoint { get; set; }
    public string? DependsOn { get; set; }

    // Properties specific to file upload
    public long? MaxFileSize { get; set; }
    public List<string>? AllowedFileTypes { get; set; } = new List<string>(); // Allowed file extensions (e.g., ".jpg", ".png")
}

// Model for sections in the form
public class FormSection
{
    [Required]
    public string SectionId { get; set; } = null!;
    
    [Required]
    public string SectionName { get; set; } = null!;
    
    [Required]
    public string SectionKey { get; set; } = null!;
    
    public bool AllowMultipleEntries { get; set; } // If multiple entries (e.g., multiple vehicles) are allowed.
    public List<FormField> Fields { get; set; } = new List<FormField>();
    public List<FormSubSection> SubSections { get; set; } = new List<FormSubSection>(); // Subsections within the section
}

// Model for subsections in the form
public class FormSubSection
{
    [Required]
    public string SubSectionId { get; set; } = null!;
    
    [Required]
    public string SubSectionName { get; set; } = null!;
    
    [Required]
    public string SubSectionKey { get; set; } = null!;
    
    [Required]
    public List<FormField> Fields { get; set; } = new List<FormField>(); // Fields within this subsection
    
    public bool AllowMultipleEntries { get; set; } // Indicates if this subsection allows multiple entries
    
    public List<FormSubSection>? SubSubSections { get; set; } = new List<FormSubSection>(); // Nested subsections (if any)
}

// Model for options (for select, radio, etc.)
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

// Enum for input types in the form
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