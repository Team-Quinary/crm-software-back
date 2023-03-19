using crm_software_back.Models;
using static crm_software_back.Controllers.PaymentController;

namespace crm_software_back.Services.PaymentServices
{
    public interface IPaymentService
    {
        public Task<Payment?> getPayment(int paymentId);
        public Task<List<Payment>?> getPayments();
        public Task<Payment?> postPayment(Payment newPayment);
        //public Task<Project?> putProject(int projectId, Project newProject);
        //public Task<Project?> deleteProject(int projectId);

        int CalculateOrderAmount(Item[] items);
    }
}
