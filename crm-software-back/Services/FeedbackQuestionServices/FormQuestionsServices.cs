using crm_software_back.Models;
using crm_software_back.Data;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.FeedbackQuestionServices
{
    public class FormQuestionsServices
    {
        private readonly DataContext _context;

        public FormQuestionsServices(DataContext context)
        {
            _context = context;
        }

        public async Task<FeedbackFormQuestions?> PostQuestions(FeedbackFormQuestions newQuestion)
        {
            var question = await _context.FormQuestions.Where(question => question.FormId.Equals(newQuestion.FormId)).FirstOrDefault();
        }
    }
}
