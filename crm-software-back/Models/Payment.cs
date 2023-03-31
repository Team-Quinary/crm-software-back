using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace crm_software_back.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public string StripeId { get; set; } = string.Empty;
    }
}
