using System.ComponentModel.DataAnnotations;

namespace crm_software_back.Models
{
    public class FeedbackForm
    {
        [Key]
        public int FormId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
