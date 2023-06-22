using crm_software_back.DTOs;
using crm_software_back.Models;

namespace crm_software_back.Services.FeedbackFormServices
{
    public interface IFeedbackFormServices
    {
        public Task<List<FeedbackForm>?> GetFeedbackForms();
        public Task<FeedbackForm?> PostFeedbackForm(FeedbackForm newFeedbackForm);
        public Task<FeedbackForm?> PutFeedbackForm(int feedbackFormId, DTOFeedbackForm newFeedbackForm);
        public Task<FeedbackForm?> DeleteFeedbackForm(int feedbackFormId);
        public Task<FeedbackForm?> SaveChanges(DTOFeedbackForm newFeedbackForm);
    }
}
