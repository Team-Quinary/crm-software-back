using EmailService;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] DTOMessage dtoMessage)
        {
            if (dtoMessage == null)
            {
                return BadRequest("No file selected");
            }

            List<ToPair>? toPairList = new List<ToPair>();

            foreach(var toPair in dtoMessage.To)
            {
                toPairList.Add(JsonSerializer.Deserialize<ToPair>(toPair));
            }

            var message = new Message(
                toPairList,
                dtoMessage.Subject,
                dtoMessage.Content,
                dtoMessage.Attachments
            );

            await _emailSender.SendEmailAsync(message);

            return Ok("File uploaded successfully");
        }
    }
}
