using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace crm_software_back.Services.LoginUserServices
{
    public class LoginUserService : ILoginUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginUser?> getLoginUser(int userId)
        {
            var loginUser = await _context.LoginUsers.FindAsync(userId);

            return loginUser;
        }

        public async Task<LoginUser?> postLoginUser(DTOUser newLoginUser)
        {
            var user = await _context.Users.Where(user =>
                user.Username.Equals(newLoginUser.Username)
            ).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var loginUser = await _context.LoginUsers.FindAsync(user.UserId);

            if (loginUser != null)
            {
                return null;
            }

            CreatePasswordHash(newLoginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            loginUser = new LoginUser()
            {
                UserId = user.UserId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.LoginUsers.Add(loginUser);
            await _context.SaveChangesAsync();

            return loginUser;
        }

        public async Task<LoginUser?> putLoginUser(int userId, DTOUser newLoginUser)
        {
            var loginUser = await _context.LoginUsers.FindAsync(userId);

            if (loginUser == null)
            {
                return null;
            }

            CreatePasswordHash(newLoginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            loginUser.PasswordHash = passwordHash;
            loginUser.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return loginUser;
        }

        public async Task<LoginUser?> deleteLoginUser(int userId)
        {
            var loginUser = await _context.LoginUsers.FindAsync(userId);

            if (loginUser == null)
            {
                return null;
            }

            _context.LoginUsers.Remove(loginUser);
            await _context.SaveChangesAsync();

            return loginUser;
        }

        public async Task<string?> login(DTOUser request)
        {
            var user = await _context.Users.Where(user =>
                user.Username.Equals(request.Username)
            ).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var loginUser = await _context.LoginUsers.FindAsync(user.UserId);

            if (loginUser == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(request.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
            {
                return null;
            }

            string token = CreateToken(user);

            return token;
        }

        public async Task<DTOLoginUser?> getTokenData()
        {
            var loginUser = new DTOLoginUser();

            if (_httpContextAccessor != null)
            {
                loginUser.Username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                loginUser.Type = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                loginUser.ExpireDate = _httpContextAccessor.HttpContext.User.FindFirstValue("exp");
            }

            var user = await _context.Users.Where(user => user.Username.Equals(loginUser.Username)).FirstOrDefaultAsync();

            var filePath = $"./ProfilePics/{user?.ProfilePic}";
            var fileExtension = Path.GetExtension(filePath);

            loginUser.FirstName = user.FirstName;
            loginUser.LastName = user.LastName;

            if (File.Exists(filePath))
            {
                //var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //loginUser.ProfilePic = new FileStreamResult(fileStream, $"image/{fileExtension[1..]}");

                var fileBytes = File.ReadAllBytes(filePath);
                loginUser.ProfilePic = new FileContentResult(fileBytes, "image/png");
            }

            return loginUser;
        }

        //public async Task<ActionResult<IFormFile?>> getProfilePic(string username)
        //{
        //    var user = await _context.Users.Where(user => user.Username.Equals(username)).FirstOrDefaultAsync();

        //    var filePath = $"./ProfilePics/{user?.ProfilePic}";
        //    var fileExtension = Path.GetExtension(filePath);

        //    if (!File.Exists(filePath))
        //    {
        //        return null;
        //    }

        //    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        //    return new FileStreamResult(fileStream, $"image/{fileExtension[1..]}");
        //}

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Type),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWTSettings:Token").Value
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
