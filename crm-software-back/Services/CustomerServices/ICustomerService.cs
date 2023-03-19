using crm_software_back.Models;

namespace crm_software_back.Services.CustomerServices
{
    public interface ICustomerService
    {
        public Task<Customer?> getCustomer(int customerId);
        public Task<List<Customer>?> getCustomers();
        public Task<Customer?> postCustomer(Customer newCustomer);
        public Task<Customer?> putCustomer(int customerId, Customer newCustomer);
        public Task<Customer?> deleteCustomer(int customerId);
    }
}
