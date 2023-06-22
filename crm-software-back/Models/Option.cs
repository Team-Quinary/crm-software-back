using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace crm_software_back.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [JsonIgnore]
        public Question? Question { get; set; }

        public string Text { get; set; }
    }
}
