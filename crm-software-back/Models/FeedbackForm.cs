using crm_software_back.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace crm_software_back.Models
{
    public class FeedbackForm
    {
        [Key]
        public int FormId { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<Question> Questions { get; set; }
    }
}
