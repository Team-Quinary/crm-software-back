using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace crm_software_back.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [ForeignKey("FeedbackForm")]
        public int FormId { get; set; }

        [BindNever]
        [JsonIgnore]
        public FeedbackForm FeedbackForm { get; set; }

        public string Text { get; set; }

        [Required, Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; }

        public bool IsRequired { get; set; }

        public List<Option> Options { get; set; }
    }
}
