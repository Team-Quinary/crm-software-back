using crm_software_back.Models;

namespace crm_software_back.Services.EnduserServices
{
    public interface IEnduserService
    {
        public Task<Sale?> getSale(int saleId);
        public Task<List<Sale>?> getSales();
        public Task<Sale?> postSale(Sale newSale);
        public Task<Sale?> putSale(int saleId, Sale newSale);
        public Task<Sale?> deleteSale(int saleId);

        public Task<Enduser?> getEnduser(int enduserId);
        public Task<List<Enduser>?> getEndusers();
        public Task<Enduser?> postEnduser(Enduser newEnduser);
        public Task<Enduser?> putEnduser(int enduserId, Enduser newEnduser);
        public Task<Enduser?> deleteEnduser(int enduserId);
    }
}
