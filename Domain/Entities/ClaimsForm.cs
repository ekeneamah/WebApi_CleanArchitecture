using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ClaimsForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Form {  get; set; }
        public string APIEndPoint { get; set; }
        public int Coy_id {  get; set; }
        public string Coy_name { get; set; }
    }
}
