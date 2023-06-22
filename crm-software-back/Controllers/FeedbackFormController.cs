using crm_software_back.Models;
using crm_software_back.Services.CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using crm_software_back.Services.FeedbackFormServices;
using Stripe;
using Stripe.TestHelpers;
using crm_software_back.Services.QuestionServices;
using crm_software_back.Services.OptionServices;
using crm_software_back.DTOs;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackFormController : ControllerBase
    {
        private readonly IFeedbackFormServices _feedbackServices;

        public FeedbackFormController(IFeedbackFormServices feedbackFormServices)
        {
            _feedbackServices = feedbackFormServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<FeedbackForm>?>> GetFeedbackForm()
        {
            var feedback = await _feedbackServices.GetFeedbackForms();

            if (feedback == null)
            {
                return NotFound("Form list is Empty..!");
            }

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackForm?>> PostFeedbackForm(FeedbackForm newFeedbackForm)
        {
            var feedback = await _feedbackServices.PostFeedbackForm(newFeedbackForm);

            if (feedback == null)
            {
                return NotFound("Form already exist..!");
            }

            return Ok(feedback);
        }

        [HttpPost("SaveChanges")]
        public async Task<ActionResult<FeedbackForm?>> SaveChanges(DTOFeedbackForm newFeedbackForm)
        {
            var feedback = await _feedbackServices.SaveChanges(newFeedbackForm);

            if (feedback != null)
            {
                return NotFound("Form does not exist..!");
            }

            return Ok(feedback);
        }

        [HttpPut("{feedbackFormId}")]
        public async Task<ActionResult<FeedbackForm?>> PutFeedbackForm(int feedbackFormId, DTOFeedbackForm newFeedbackForm)
        {
            var feedback = await _feedbackServices.PutFeedbackForm(feedbackFormId, newFeedbackForm);

            if (feedback == null)
            {
                return NotFound("Form does not exist..!");
            }

            return Ok(feedback);
        }

        [HttpDelete("{feedbackFormId}")]
        public async Task<ActionResult<FeedbackForm?>> DeleteFeedbackForm(int feedbackFormId)
        {
            var feedback = await _feedbackServices.DeleteFeedbackForm(feedbackFormId);

            if (feedback == null)
            {
                return NotFound("Feedback Form is not found..!");
            }

            return Ok(feedback);
        }
    }
}
