using crm_software_back.DTOs;
using crm_software_back.Models;

namespace crm_software_back.Services.LoginUserServices
{
    public interface ILoginUserService
    {
        Task<LoginUser?> getLoginUser(int userId);
        Task<LoginUser?> postLoginUser(DTOUser newLoginUser);
        Task<LoginUser?> putLoginUser(int userId, DTOUser newLoginUser);
        Task<LoginUser?> deleteLoginUser(int userId);
        Task<string?> login(DTOUser user);
        Task<bool> authenticateUser(DTOUser request);
        Task<DTOLoginUser?> getTokenData();
    }
}
