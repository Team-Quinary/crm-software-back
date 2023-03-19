using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.LoginUserServices;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly ILoginUserService _loginUserService;

        public UserService(DataContext context, ILoginUserService loginUserService)
        {
            _context = context;
            _loginUserService = loginUserService;
        }

        public async Task<User?> getUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user;
        }

        public async Task<List<User>?> getUsers()
        {
            var notes = await _context.Users.ToListAsync();

            return notes;
        }

        public async Task<User?> postUser(User newUser)
        {
            var user = await _context.Users.Where(user => 
                user.Username.Equals(newUser.Username) || user.Email.Equals(newUser.Email)
            ).FirstOrDefaultAsync();

            if (user != null)
            {
                return null;
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newDTOuser = new DTOUser()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

            await _loginUserService.postLoginUser(newDTOuser);

            return await _context.Users.Where(user => user.Email.Equals(newUser.Email)).FirstOrDefaultAsync();
        }

        public async Task<User?> putUser(int userId, User newUser)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            user.Type = (newUser.Type == "") ? user.Type : newUser.Type;
            user.Username = (newUser.Username == "") ? user.Username : newUser.Username;
            user.Password = (newUser.Password == "") ? user.Password : newUser.Password;
            user.FirstName = (newUser.FirstName == "") ? user.FirstName : newUser.FirstName;
            user.LastName = (newUser.LastName == "") ? user.LastName : newUser.LastName;
            user.ContactNo = (newUser.ContactNo == "") ? user.ContactNo : newUser.ContactNo;
            user.Email = (newUser.Email == "") ? user.Email : newUser.Email;
            user.ProfilePic = (newUser.ProfilePic == "") ? user.ProfilePic : newUser.ProfilePic;

            await _context.SaveChangesAsync();

            var newDTOuser = new DTOUser()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

            await _loginUserService.putLoginUser(userId, newDTOuser);

            return user;
        }

        public async Task<User?> deleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
