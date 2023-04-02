using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_software_back.Models
{
    public class FeedbackFormQuestion
    {
        [Key]
        public int QuestionId { get; set; }

        [ForeignKey("FeedbackForm")]
        public int FormId { get; set; }

        public FeedbackForm FeedbackForm { get; set; }

        public string Question { get; set; }

        public string QuestionType { get; set; }
    }
    }

