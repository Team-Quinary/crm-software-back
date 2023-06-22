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
            var answer = await _context.Answers.Where(answer => 
                answer.QuestionId == newAnswer.QuestionId && answer.ProjectId == newAnswer.ProjectId && answer.Text == newAnswer.Text
            ).FirstOrDefaultAsync();

            if (answer != null)
            {
                return null;
            }

            _context.Answers.Add(newAnswer);
            await _context.SaveChangesAsync();

            return await _context.Answers.Where(answer => 
                answer.ProjectId == newAnswer.QuestionId && answer.ProjectId == newAnswer.ProjectId && answer.Text == newAnswer.Text
            ).FirstOrDefaultAsync();

        }

        public async Task<List<Answer>?> GetAnswers()
        {
            var answers = await _context.Answers.ToListAsync();

            return answers;
        }

        public Task<Answer?> PutAnswer(int answerId, Answer newAnswer)
        {
            throw new NotImplementedException();
        }

        public Task<Answer?> DeleteAnswer(int answerId)
        {
            throw new NotImplementedException();
        }
    }
}



