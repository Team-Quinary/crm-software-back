using crm_software_back.Models;

namespace crm_software_back.Services.FeedbackQuestionServices
{
    public interface IFromQuestionServices
    {
        public Task<FeedbackFormQuestion?> PostQuestion(FeedbackFormQuestion newQuestion);
        public Task<List<FeedbackFormQuestion>?> GetQuestion();
    }
}
