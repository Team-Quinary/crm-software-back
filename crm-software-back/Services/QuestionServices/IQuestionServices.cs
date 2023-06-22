using crm_software_back.DTOs;
using crm_software_back.Models;

namespace crm_software_back.Services.QuestionServices
{
    public interface IQuestionServices
    {
        public Task<List<Question>?> GetQuestions(int formId);
        public Task<Question?> PostQuestion(DTOQuestion newQuestion);
        public Task<Question?> PutQuestion(int questionId, DTOQuestion newQuestion);
        public Task<Question?> DeleteQuestion(int questionId);
    }
}
