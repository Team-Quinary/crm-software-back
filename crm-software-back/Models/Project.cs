using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace crm_software_back.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = string.Empty;

        public int Fee { get; set; }

        public int Duration { get; set; }

        public DateTime StartDate { get; set; }

        public int Installments { get; set; }

        [Required, Column(TypeName = "nvarchar(10)")]
        public string Status { get; set; } = string.Empty;

        [Required, Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }

        [ForeignKey("User")]
        public int TechLeadId { get; set; }

        [JsonIgnore]
        public User? TechLead { get; set; }
    }
}
