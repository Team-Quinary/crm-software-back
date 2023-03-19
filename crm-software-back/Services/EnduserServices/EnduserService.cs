using crm_software_back.Data;
using crm_software_back.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace crm_software_back.Services.EnduserServices
{
    public class EnduserService : IEnduserService
    {
        private readonly DataContext _dataContext;

        public EnduserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Enduser?> getEnduser(int enduserId)
        {
            var endUsers = await _dataContext.Endusers.FindAsync(enduserId);

            return endUsers;
        }

        public async Task<List<Enduser>?> getEndusers()
        {
            var endUser = await _dataContext.Endusers.ToListAsync();

            return endUser;
        }

        public async Task<Enduser?> postEnduser(Enduser newEnduser)
        {
            var endUser = await _dataContext.Endusers.Where(endUser => endUser.Company == newEnduser.Company).FirstOrDefaultAsync();
            
            if (endUser != null)
            {
                return null;
            }

            _dataContext.Endusers.Add(newEnduser);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.Endusers.Where(endUser => endUser.Company == newEnduser.Company).FirstOrDefaultAsync(); ;
        }

        public async Task<Enduser?> putEnduser(int enduserId, Enduser newEnduser)
        {
            var endUser = await _dataContext.Endusers.FindAsync(enduserId);

            if (endUser == null)
            {
                return null;
            }

            endUser.Company = (newEnduser.Company == "") ? endUser.Company : newEnduser.Company;
            
            await _dataContext.SaveChangesAsync();

            return endUser;
        }

        public async Task<Enduser?> deleteEnduser(int enduserId)
        {
            var endUser = await _dataContext.Endusers.FindAsync(enduserId);

            if (endUser == null)
            {
                return null;
            }

            _dataContext.Endusers.Remove(endUser);
            await _dataContext.SaveChangesAsync();

            return endUser;
        }

        public async Task<Sale?> getSale(int saleId)
        {
            var sale = await _dataContext.Sales.FindAsync(saleId);

            if (sale != null)
            {
                sale.Project = await _dataContext.Projects.FindAsync(sale.ProjectId);
                sale.Enduser = await _dataContext.Endusers.FindAsync(sale.EnduserId);
            }

            return sale;
        }

        public async Task<List<Sale>?> getSales()
        {
            var sales = await _dataContext.Sales.ToListAsync();

            if (sales != null)
            {
                var newSales = new List<Sale>();
                foreach(var sale in sales)
                {
                    sale.Project = await _dataContext.Projects.FindAsync(sale.ProjectId);
                    sale.Enduser = await _dataContext.Endusers.FindAsync(sale.EnduserId);

                    newSales.Add(sale);
                }

                return newSales;
            }

            return sales;
        }

        public async Task<Sale?> postSale(Sale newSale)
        {
            var sale = await _dataContext.Sales.Where(sale => sale.ProjectId == newSale.ProjectId && sale.EnduserId == newSale.EnduserId).FirstOrDefaultAsync();
            
            if (sale != null)
            {
                return null;
            }

            _dataContext.Sales.Add(newSale);
            await _dataContext.SaveChangesAsync();

            sale = await _dataContext.Sales.Where(sale => sale.ProjectId == newSale.ProjectId && sale.EnduserId == newSale.EnduserId).FirstOrDefaultAsync();

            if (sale != null)
            {
                sale.Project = await _dataContext.Projects.FindAsync(sale.ProjectId);
                sale.Enduser = await _dataContext.Endusers.FindAsync(sale.EnduserId);
            }

            return sale;
        }

        public async Task<Sale?> putSale(int saleId, Sale newSale)
        {
            var sale = await _dataContext.Sales.FindAsync(saleId);

            if (sale == null)
            {
                return null;
            }

            sale.SoldDate = (newSale.SoldDate == new DateTime()) ? sale.SoldDate : newSale.SoldDate;
            sale.ProjectId = (newSale.ProjectId == 0) ? sale.ProjectId : newSale.ProjectId;
            sale.EnduserId = (newSale.EnduserId == 0) ? sale.EnduserId : newSale.EnduserId;

            await _dataContext.SaveChangesAsync();

            if (sale != null)
            {
                sale.Project = await _dataContext.Projects.FindAsync(sale.ProjectId);
                sale.Enduser = await _dataContext.Endusers.FindAsync(sale.EnduserId);
            }

            return sale;
        }

        public async Task<Sale?> deleteSale(int saleId)
        {
            var sale = await _dataContext.Sales.FindAsync(saleId);

            if (sale == null)
            {
                return null;
            }

            _dataContext.Sales.Remove(sale);
            await _dataContext.SaveChangesAsync();

            return sale;
        }
    }
}
