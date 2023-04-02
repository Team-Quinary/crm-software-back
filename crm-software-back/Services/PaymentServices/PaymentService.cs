using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using Microsoft.CodeAnalysis;
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
            var payments = await _context.Payments
                .Include(p => p.Project)
                .ThenInclude(p => (p != null) ? p.Customer : null)
                .ToListAsync();

            return payments;
        }

        public async Task<Payment?> postPayment(DTOPayment newPayment)
        {
            var payment = await _context.Payments.Where(payment => payment.StripeId == newPayment.StripeId).FirstOrDefaultAsync();

            if (payment != null || newPayment.ProjectId == 0 || newPayment.StripeId == null)
            {
                return null;
            }

            var paymentData = await getPaymentData(newPayment.ProjectId);

            if (paymentData == null)
            {
                return null;
            }

            var paid = new Payment
            {
                ProjectId = newPayment.ProjectId,
                Amount = paymentData.NextInstallment,
                Date = DateTime.Now,
                StripeId = newPayment.StripeId
            };

            _context.Payments.Add(paid);
            await _context.SaveChangesAsync();

            payment = await _context.Payments.Where(payment => payment.StripeId == newPayment.StripeId).FirstOrDefaultAsync();

            if (payment != null)
            {
                payment.Project = await _context.Projects.FindAsync(payment.ProjectId);
                
                if (payment.Project != null)
                {
                    payment.Project.Customer = await _context.Customers.FindAsync(payment.Project?.CustomerId);
                }
            }

            return payment;
        }

        public async Task<DTOPaymentData?> getPaymentData(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return null;
            }

            var payments = await _context.Payments.Where(payment => payment.ProjectId == projectId).ToListAsync();

            double totalPaid = 0;
            var count = 0;
            var lastPaymentDate = new DateTime();
            double lastPayment = 0;

            if (payments != null)
            {
                foreach (var payment in payments)
                {
                    totalPaid += payment.Amount;
                    count++;
                    if (lastPaymentDate < payment.Date)
                    {
                        lastPaymentDate = payment.Date;
                        lastPayment = payment.Amount;
                    }
                }
            }

            double next = ((double) project.Fee) / project.Installments;
            double payable = project.Fee - totalPaid;

            var paymentDetails = new DTOPaymentData()
            {
                TotalFee = project.Fee,
                Installments = project.Installments,
                PaybleAmount = payable,
                NextInstallment = (next > payable) ? Math.Round(payable, 2) : Math.Round(next, 2),
                DueDate = project.StartDate.AddMonths(count+1),
                LastPayment = Math.Round(lastPayment, 2),
                LastPaymentDate = lastPaymentDate
            };

            return paymentDetails;
        }
    }
}
