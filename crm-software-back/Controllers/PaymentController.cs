using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Stripe;
using crm_software_back.Models;
using crm_software_back.Services.PaymentServices;
using crm_software_back.DTOs;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Stripe/{projectId}")]
        public async Task<ActionResult> Create(int projectId)
        {
            var paymentData = await _paymentService.getPaymentData(projectId);

            if (paymentData == null)
            {
                return NotFound();
            }

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = (int) (paymentData.NextInstallment*100),
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            var responseObject = new { clientSecret = paymentIntent.ClientSecret };
            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(responseObject);

            return Content(jsonResponse, "application/json");
        }

        [HttpPost("Project/{projectId}")]
        public async Task<ActionResult<DTOPaymentData?>> getPaymentData(int projectId)
        {
            var result = await _paymentService.getPaymentData(projectId);

            return Ok(result);
        }

        [HttpGet("{paymentId}")]
        public async Task<ActionResult<Payment?>> getPayment(int paymentId)
        {
            var payment = await _paymentService.getPayment(paymentId);

            if (payment == null)
            {
                return NotFound("Payment does not exist");
            }

            return Ok(payment);
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>?>> getPayments()
        {
            var payments = await _paymentService.getPayments();

            if (payments == null)
            {
                return NotFound("Payments list is Empty..!");
            }

            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<Payment?>> postPayment(DTOPayment newPayment)
        {
            var payment = await _paymentService.postPayment(newPayment);

            if (payment == null)
            {
                return NotFound("Payment is already exist..!");
            }

            return Ok(payment);
        }
    }
}
