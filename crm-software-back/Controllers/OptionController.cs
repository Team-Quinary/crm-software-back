using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.OptionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionServices _optionServices;

        public OptionController(IOptionServices optionServices)
        {
            _optionServices = optionServices;
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<List<Option>?>> GetOptions(int questionId)
        {
            var option = await _optionServices.GetOptions(questionId);

            if (option == null)
            {
                return NotFound("Option list is Empty..!");
            }

            return Ok(option);
        }

        [HttpPost]
        public async Task<ActionResult<Option?>> PostOption(DTOOption newOption)
        {
            var option = await _optionServices.PostOption(newOption);

            if (option == null)
            {
                return NotFound("Option already exist..!");
            }

            return Ok(option);
        }

        [HttpPut("{optionId}")]
        public async Task<ActionResult<Option?>> PutOption(int optionId, DTOOption newOption)
        {
            var option = await _optionServices.PutOption(optionId, newOption);

            if (option == null)
            {
                return NotFound("Option does not exist..!");
            }

            return Ok(option);
        }

        [HttpDelete("{optionId}")]
        public async Task<ActionResult<Option?>> DeleteOption(int optionId)
        {
            var option = await _optionServices.DeleteOption(optionId);

            if (option == null)
            {
                return NotFound("Option is not found..!");
            }

            return Ok(option);
        }
    }
}
