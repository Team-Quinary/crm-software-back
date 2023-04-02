using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_software_back.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [ForeignKey("FeedbackForm")]
        public int FormId { get; set; }

        public FeedbackForm FeedbackForm { get; set; }

        [ForeignKey("FeedbackFormQuestion")]
        public int QuestionId { get; set; }

        public FeedbackFormQuestion FeedbackFormQuestion { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string QuesAnswer { get; set; }
    }
}
