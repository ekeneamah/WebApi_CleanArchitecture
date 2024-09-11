using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ClaimsUnderWritingForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public string Id { get; set; } = null!;
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
        get => JsonSerializer.Serialize(new { Form = GlobalFields, Sections },
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase,     Converters = { new JsonStringEnumConverter() }});
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
