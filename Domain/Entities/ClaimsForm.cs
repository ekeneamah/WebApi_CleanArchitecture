using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ClaimsForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Form {  get; set; }
        public string ApiEndPoint { get; set; }
        public int CoyId {  get; set; }
        public string CoyName { get; set; }
    }
}
