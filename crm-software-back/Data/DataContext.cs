using crm_software_back.Models;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<LoginUser> LoginUsers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Enduser> Endusers { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
