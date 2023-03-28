using crm_software_back.Data;
using crm_software_back.DTOs;
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
            var payments = await _context.Payments
                .Include(p => p.Project)
                .ThenInclude(p => (p != null) ? p.Customer : null)
                .ToListAsync();

            //if (payments != null)
            //{
            //    var newPayments = new List<Payment>();
            //    foreach (Payment payment in payments)
            //    {
            //        payment.Project = await _context.Projects.FindAsync(payment.ProjectId);

            //        payment.Project.Customer = _context.Customers.Find(payment?.Project?.CustomerId);
                    
            //        newPayments.Add(payment);
            //    }

            //    return newPayments;
            //}

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

        public async Task<DTOPaymentData?> getPaymentData(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return null;
            }

            var payments = await _context.Payments.Where(payment => payment.ProjectId == projectId).ToListAsync();

            var totalPaid = 0;
            var count = 0;
            var lastPaymentDate = new DateTime();
            var lastPayment = 0;

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

            //var now = DateTime.Now;
            //var elapsedMonths = (now.Year - project.StartDate.Year) * 12 + now.Month - project.StartDate.Month;

            var paymentDetails = new DTOPaymentData()
            {
                TotalFee = project.Fee,
                Installments = project.Installments,
                PaybleAmount = project.Fee - totalPaid,
                NextInstallment = project.Fee / project.Installments,
                DueDate = project.StartDate.AddMonths(count+1),
                LastPayment = lastPayment,
                LastPaymentDate = lastPaymentDate
            };

            return paymentDetails;
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
