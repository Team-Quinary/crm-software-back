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

        public async Task<FeedbackFormQuestion?> PostQuestion(FeedbackFormQuestion newQuestion)
        {
            var question = await _context.FormQuestions.Where(question => question.FormId.Equals(newQuestion.FormId)).FirstOrDefaultAsync();

            if (question != null)
            {
                return null;
            }

            _context.FormQuestions.Add(newQuestion);
            await _context.SaveChangesAsync();
            return await _context.FormQuestions.Where(question => question.FormId.Equals(newQuestion.FormId)).FirstOrDefaultAsync();



        }
        public async Task<List<FeedbackFormQuestion>?> GetQuestion()
        {
            var question = await _context.FormQuestions.ToListAsync();

            return question;
        }
    }
}
