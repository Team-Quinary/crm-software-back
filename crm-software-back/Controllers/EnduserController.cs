using crm_software_back.Data;
using crm_software_back.Models;
using crm_software_back.Services.EnduserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnduserController : ControllerBase
    {
        private readonly IEnduserService _enduserService;

        public EnduserController(IEnduserService enduserService)
        {
            _enduserService = enduserService;
        }

        [HttpGet("Sale/{saleId}")]
        public async Task<ActionResult<Sale?>> getSale(int saleId)
        {
            var sale = await _enduserService.getSale(saleId);

            if (sale == null)
            {
                return NotFound("Sale does not exist");
            }

            return Ok(sale);
        }

        [HttpGet("Sale")]
        public async Task<ActionResult<List<Sale>?>> getSales()
        {
            var sales = await _enduserService.getSales();

            if (sales == null)
            {
                return NotFound("Sales list is Empty..!");
            }

            return Ok(sales);
        }

        [HttpPost("Sale")]
        public async Task<ActionResult<Sale?>> postSale(Sale newSale)
        {
            var sale = await _enduserService.postSale(newSale);

            if (sale == null)
            {
                return NotFound("Sale is already exist..!");
            }

            return Ok(sale);
        }

        [HttpPut("Sale/{saleId}")]
        public async Task<ActionResult<Sale?>> putSale(int saleId, Sale newSale)
        {
            var sale = await _enduserService.putSale(saleId, newSale);

            if (sale == null)
            {
                return NotFound("Sale is not found..!");
            }

            return Ok(sale);
        }

        [HttpDelete("Sale/{saleId}")]
        public async Task<ActionResult<Sale?>> deleteSale(int saleId)
        {
            var sale = await _enduserService.deleteSale(saleId);

            if (sale == null)
            {
                return NotFound("Sale is not found..!");
            }

            return Ok(sale);
        }

        
        [HttpGet("{enduserId}")]
        public async Task<ActionResult<Enduser?>> getEnduser(int enduserId)
        {
            var endUser = await _enduserService.getEnduser(enduserId);

            if (endUser == null)
            {
                return NotFound("Enduser does not exist");
            }

            return Ok(endUser);
        }

        [HttpGet]
        public async Task<ActionResult<List<Enduser>?>> getEndusers()
        {
            var endUsers = await _enduserService.getEndusers();

            if (endUsers == null)
            {
                return NotFound("Endusers list is Empty..!");
            }

            return Ok(endUsers);
        }

        [HttpPost]
        public async Task<ActionResult<Enduser?>> postEnduser(Enduser newEnduser)
        {
            var endUser = await _enduserService.postEnduser(newEnduser);

            if (endUser == null)
            {
                return NotFound("Enduser is already exist..!");
            }

            return Ok(endUser);
        }

        [HttpPut("{enduserId}")]
        public async Task<ActionResult<Enduser?>> putEnduser(int enduserId, Enduser newEnduser)
        {
            var endUser = await _enduserService.putEnduser(enduserId, newEnduser);

            if (endUser == null)
            {
                return NotFound("Enduser is not found..!");
            }

            return Ok(endUser);
        }

        [HttpDelete("{enduserId}")]
        public async Task<ActionResult<Enduser?>> deleteEnduser(int enduserId)
        {
            var endUser = await _enduserService.deleteEnduser(enduserId);

            if (endUser == null)
            {
                return NotFound("Enduser is not found..!");
            }

            return Ok(endUser);
        }
    }
}
