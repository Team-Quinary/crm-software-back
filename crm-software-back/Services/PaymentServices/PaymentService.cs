using crm_software_back.Data;
using crm_software_back.Models;
using Microsoft.EntityFrameworkCore;
using static crm_software_back.Controllers.PaymentController;

namespace crm_software_back.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext _context;

        public PaymentService(DataContext context)
        {
            _context = context;
        }

        public async Task<Payment?> getPayment(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment != null)
            {
                payment.Project = await _context.Projects.FindAsync(payment.ProjectId);
            }

            return payment;
        }

        public async Task<List<Payment>?> getPayments()
        {
            var payments = await _context.Payments.ToListAsync();

            if (payments != null)
            {
                var newPayments = new List<Payment>();
                foreach (Payment payment in payments)
                {
                    payment.Project = await _context.Projects.FindAsync(payment.ProjectId);
                    
                    newPayments.Add(payment);
                }

                return newPayments;
            }

            return payments;
        }

        public async Task<Payment?> postPayment(Payment newPayment)
        {
            var payment = await _context.Payments.Where(payment => payment.PaymentId == newPayment.PaymentId).FirstOrDefaultAsync();

            if (payment != null)
            {
                return null;
            }

            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();

            payment = await _context.Payments.Where(payment => payment.PaymentId == newPayment.PaymentId).FirstOrDefaultAsync();

            if (payment != null)
            {
                payment.Project = await _context.Projects.FindAsync(payment.ProjectId);
            }

            return payment;
        }

        public int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 5000;
        }
    }
}
