using crm_software_back.DTOs;
using crm_software_back.Models;

namespace crm_software_back.Services.UserServices
{
    public interface IUserService
    {
        public Task<User?> getUser(int userId);
        public Task<List<User>?> getUsers();
        public Task<User?> postUser(User newUser);
        public Task<User?> putUser(int userId, User newUser);
        public Task<User?> deleteUser(int userId);
        public Task<DTODashBoard> getDashboardData();
    }
}
