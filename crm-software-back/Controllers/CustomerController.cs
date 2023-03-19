using crm_software_back.Models;
using crm_software_back.Services.CustomerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer?>> getCustomer(int customerId)
        {
            var customer = await _customerService.getCustomer(customerId);

            if (customer == null)
            {
                return NotFound("Customer does not exist");
            }

            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>?>> getCustomers()
        {
            var customers = await _customerService.getCustomers();

            if (customers == null)
            {
                return NotFound("Customers list is Empty..!");
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer?>> postCustomer(Customer newCustomer)
        {
            var customer = await _customerService.postCustomer(newCustomer);

            if (customer == null)
            {
                return NotFound("Customer is already exist..!");
            }

            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult<Customer?>> putCustomer(int customerId, Customer newCustomer)
        {
            var customer = await _customerService.putCustomer(customerId, newCustomer);

            if (customer == null)
            {
                return NotFound("Customer is not found..!");
            }

            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<Customer?>> deleteCustomer(int customerId)
        {
            var customer = await _customerService.deleteCustomer(customerId);

            if (customer == null)
            {
                return NotFound("Customer is not found..!");
            }

            return Ok(customer);
        }
    }
}
