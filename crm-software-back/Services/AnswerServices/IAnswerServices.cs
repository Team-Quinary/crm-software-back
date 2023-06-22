using crm_software_back.Models;

namespace crm_software_back.Services.AnswerServices
{
    public interface IAnswerServices
    {
        public Task<List<Answer>?> GetAnswers();
        public Task<Answer?> PostAnswer(Answer newAnswer);
        public Task<Answer?> PutAnswer(int answerId, Answer newAnswer);
        public Task<Answer?> DeleteAnswer(int answerId);
    }
}
