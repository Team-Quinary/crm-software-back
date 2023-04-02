using crm_software_back.Models;
using crm_software_back.Services.CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using crm_software_back.Services.FeedbackFormServices;
using Stripe;
using Stripe.TestHelpers;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackFormController : ControllerBase
    {
        private readonly IFeedackFormServices _FeedbackServices;

        public FeedbackFormController(IFeedackFormServices feedbackFormServices)
        {
            _FeedbackServices = feedbackFormServices;
        }
        [HttpPost]
        public async Task<ActionResult<FeedbackForm?>> PostFeedbackForm(FeedbackForm newFeedbackForm)
        {
            var feedback = await _FeedbackServices.PostFeedbackForm(newFeedbackForm);

            if (feedback == null)
            {
                return NotFound("Form already exist..!");
            }

            return Ok(feedback);
        }
        [HttpGet]
        public async Task<ActionResult<List<FeedbackForm>?>> GetFeedbackForm()
        {
            var feedback = await _FeedbackServices.GetFeedbackForms();

            if (feedback == null)
            {
                return NotFound("Form list is Empty..!");
            }

            return Ok(feedback);
        }
    }
}
