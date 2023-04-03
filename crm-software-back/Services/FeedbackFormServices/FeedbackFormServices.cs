using crm_software_back.Data;
using crm_software_back.Models;
using crm_software_back.Services.FeedbackFormServices;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.FormServices
{
    public class FeedbackFormServices : IFeedackFormServices
    {
        private readonly DataContext _context;

        public FeedbackFormServices(DataContext context)
        {
            _context = context;
        }
        public async Task<FeedbackForm?> PostFeedbackForm(FeedbackForm newFeedbackForm)
        {
            var feedback = await _context.FeedbackForms.Where(feedback => feedback.Name.Equals(newFeedbackForm.Name)).FirstOrDefaultAsync();

            if (feedback != null)
            {
                return null;
            }

            _context.FeedbackForms.Add(newFeedbackForm);
            await _context.SaveChangesAsync();

            return await _context.FeedbackForms.Where(feedback => feedback.Name.Equals(newFeedbackForm.Name)).FirstOrDefaultAsync();
        }
        public async Task<List<FeedbackForm>?> GetFeedbackForms()
        {
            var feedbackForms = await _context.FeedbackForms.ToListAsync();

            return feedbackForms;
        }
    }
}
