using crm_software_back.Models;

namespace crm_software_back.Services.AnswerServices
{
    public interface IAnswerServices
    {
        public Task<Answer?> PostAnswer(Answer newAnswer);
        public Task<List<Answer>?> GetAnswer();
    }
}
