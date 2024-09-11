using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class FormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public string FormId { get; set; } = null!;    
    [NotMapped]
    [JsonIgnore]
    public List<FormAnswer> GlobalFieldAnswers { get; set; } = null!;
    [NotMapped]
    [JsonIgnore]
    public List<SectionSubmission>? SectionSubmissions { get; set; }
    
    [JsonIgnore] 
    public Product Product { get; set; } = null!;

    public DateTime SubmissionDate { get; set; }

    public string UserId { get; set; } = null!;
    public SubmissionStatus Status { get; set; }   
    public InspectionStatus? InspectionStatus { get; set; }   
   
    [Required]
    public string AnswerJson
    {
        get => JsonSerializer.Serialize(new { Form = GlobalFieldAnswers, SectionSubmissions }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase,     Converters = { new JsonStringEnumConverter() }});
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var deserializedForm = JsonSerializer.Deserialize<FormAnswerBody>(value);
                if (deserializedForm != null)
                {
                    GlobalFieldAnswers = deserializedForm.GlobalFieldAnswers ?? new List<FormAnswer>();
                    SectionSubmissions = deserializedForm.SectionSubmissions;
                }
            }
            else
            {
                GlobalFieldAnswers = new List<FormAnswer>();
                SectionSubmissions = new List<SectionSubmission>();
            }
        }
    }
}


public class SectionSubmission
{
    public string SectionId { get; set; }
    public string SectionKey { get; set; } 
    // If multiple entries are allowed, store each entry separately
    public List<SectionEntrySubmission> EntrySubmissions { get; set; }
}


public class ClaimsFormSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public string FormId { get; set; } = null!;    
    [NotMapped]
    [JsonIgnore]
    public List<FormAnswer> GlobalFieldAnswers { get; set; } = null!;
    [NotMapped]
    [JsonIgnore]
    public List<SectionSubmission>? SectionSubmissions { get; set; }
    
    [JsonIgnore] 
    public Product Product { get; set; } = null!;

    public DateTime SubmissionDate { get; set; }

    public string UserId { get; set; } = null!;
    public SubmissionStatus Status { get; set; }   
    public InspectionStatus? InspectionStatus { get; set; }   
   
    [Required]
    public string AnswerJson
    {
        get => JsonSerializer.Serialize(new { Form = GlobalFieldAnswers, SectionSubmissions }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase,     Converters = { new JsonStringEnumConverter() }});
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var deserializedForm = JsonSerializer.Deserialize<FormAnswerBody>(value);
                if (deserializedForm != null)
                {
                    GlobalFieldAnswers = deserializedForm.GlobalFieldAnswers ?? new List<FormAnswer>();
                    SectionSubmissions = deserializedForm.SectionSubmissions;
                }
            }
            else
            {
                GlobalFieldAnswers = new List<FormAnswer>();
                SectionSubmissions = new List<SectionSubmission>();
            }
        }
    }
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


public class FormAnswerBody
{
    public List<FormAnswer> GlobalFieldAnswers { get; set; } = new List<FormAnswer>(); // Questions that don't belong to a section.
    public List<SectionSubmission> SectionSubmissions { get; set; } = new List<SectionSubmission>();
}

public class SectionEntrySubmission
{
    public List<FormAnswer> FieldResponses { get; set; }
    public List<SubSectionSubmission> SubSectionSubmissions { get; set; }
}


public class SubSectionSubmission
{
    public string SubSectionId { get; set; }
    public List<FormAnswer> FieldResponses { get; set; }
}


public enum SubmissionStatus
{
    Draft,
    Submitted
}

public enum InspectionStatus
{
    NotStarted,
    Successful,
    Failure
}
