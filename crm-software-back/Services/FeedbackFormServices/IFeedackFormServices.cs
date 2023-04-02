using crm_software_back.Models;

namespace crm_software_back.Services.FeedbackFormServices
{
    public interface IFeedackFormServices
    {
        public Task<FeedbackForm?> PostFeedbackForm(FeedbackForm newFeedbackForm);
        public Task<List<FeedbackForm>?> GetFeedbackForms();
    }
}
