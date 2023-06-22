using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.QuestionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionServices _questionServices;

        public QuestionController(IQuestionServices questionServices)
        {
            _questionServices = questionServices;
        }

        [HttpGet("{formId}")]
        public async Task<ActionResult<List<Question>?>> GetQuestions(int formId)
        {
            var question = await _questionServices.GetQuestions(formId);

            if (question == null)
            {
                return NotFound("Question list is Empty..!");
            }

            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<Question?>> PostQuestion(DTOQuestion newQuestion)
        {
            var question = await _questionServices.PostQuestion(newQuestion);

            if (question == null)
            {
                return NotFound("Question already exist..!");
            }

            return Ok(question);
        }

        [HttpPut("{questionId}")]
        public async Task<ActionResult<Question?>> PutQuestion(int questionId, DTOQuestion newQuestion)
        {
            var question = await _questionServices.PutQuestion(questionId, newQuestion);

            if (question == null)
            {
                return NotFound("Question does not exist..!");
            }

            return Ok(question);
        }

        [HttpDelete("{questionId}")]
        public async Task<ActionResult<Question?>> DeleteQuestion(int questionId)
        {
            var question = await _questionServices.DeleteQuestion(questionId);

            if (question == null)
            {
                return NotFound("Question is not found..!");
            }

            return Ok(question);
        }
    }
}
