using crm_software_back.Data;
using crm_software_back.Models;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context) 
        {
            _context = context;
        }

        public async Task<Customer?> getCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            return customer;
        }

        public async Task<List<Customer>?> getCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<Customer?> postCustomer(Customer newCustomer)
        {
            var customer = await _context.Customers.Where(customer => customer.Company.Equals(newCustomer.Company)).FirstOrDefaultAsync();

            if (customer != null)
            {
                return null;
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return await _context.Customers.Where(customer => customer.Company.Equals(newCustomer.Company)).FirstOrDefaultAsync();
        }

        public async Task<Customer?> putCustomer(int customerId, Customer newCustomer)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return null;
            }

            customer.Company = (newCustomer.Company == "") ? customer.Company : newCustomer.Company;

            await _context.SaveChangesAsync();

            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<Customer?> deleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return null;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
