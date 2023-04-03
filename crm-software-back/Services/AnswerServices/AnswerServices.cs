using crm_software_back.Models;
using crm_software_back.Data;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.AnswerServices
{
    public class AnswerServices : IAnswerServices
    {

             private readonly DataContext _context;

            public AnswerServices(DataContext context)
            {
                _context = context;
            }
            public async Task<Answer?> PostAnswer(Answer newAnswer)
            {
                var answer = await _context.Answers.Where(answer => answer.FormId == newAnswer.FormId && answer.QuestionId == newAnswer.QuestionId).FirstOrDefaultAsync();

                if (answer != null)
                {
                    return null;
                }

                _context.Answers.Add(newAnswer);
                await _context.SaveChangesAsync();

                return await _context.Answers.Where(answer => answer.FormId == newAnswer.FormId && answer.QuestionId == newAnswer.QuestionId).FirstOrDefaultAsync();

            }
            public async Task<List<Answer>?> GetAnswer()
            {
                var Answers = await _context.Answers.ToListAsync();

                return Answers;
            }
    }
}



