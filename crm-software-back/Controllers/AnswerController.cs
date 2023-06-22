using crm_software_back.Models;
using crm_software_back.Services.AnswerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerServices _AnswerServices;

        public AnswerController(IAnswerServices answerServices)
        {
            _AnswerServices = answerServices;
        }
        [HttpPost]
        public async Task<ActionResult<Answer?>> PostAnswer(Answer newAnswer)
        {
            var answer = await _AnswerServices.PostAnswer(newAnswer);

            if (answer == null)
            {
                return NotFound("Answer already exist..!");
            }

            return Ok(answer);
        }

        [HttpGet]
        public async Task<ActionResult<List<Answer>?>> GetAnswer()
        {
            var answer = await _AnswerServices.GetAnswers();

            if (answer == null)
            {
                return NotFound("Answer list is Empty..!");
            }

            return Ok(answer);
        }
    }
}
