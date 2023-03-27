using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.LoginUserServices;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Security.Cryptography;
using crm_software_back.Services.UserServices;
using crm_software_back.Services.EmailService;
using BCrypt.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Net;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, DataContext dataContext, IConfiguration configuration, IEmailService emailService)
        {
            _userService = userService;
            _dataContext = dataContext;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User?>> getUser(int userId)
        {
            var user = await _userService.getUser(userId);

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>?>> getUsers()
        {
            var users = await _userService.getUsers();

            if (users == null)
            {
                return NotFound("Users list is Empty..!");
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User?>> postUser(User newUser)
        {
            var user = await _userService.postUser(newUser);

            if (user == null)
            {
                return NotFound("User is already exist..!");
            }

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<User?>> putUser(int userId, User newUser)
        {
            var user = await _userService.putUser(userId, newUser);

            if (user == null)
            {
                return NotFound("User is not found..!");
            }

            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<User?>> deleteUser(int userId)
        {
            var user = await _userService.deleteUser(userId);

            if (user == null)
            {
                return NotFound("User is not found..!");
            }

            return Ok(user);
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email doesn't exist."
                });
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            var firstName = user.FirstName;
            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Reset Password!", EmailBody.EmailStringBody1(email, emailToken, firstName));
            _emailService.SendEmail(emailModel);
            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email sent!"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _dataContext.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email doesn't exist."
                });
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid Reset Link"
                });
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "New password and confirm password do not match."
                });
            }

            user.Password = resetPasswordDto.NewPassword;
            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Password Reset Successfully."
            });
        }

        [HttpPost("send-email-informing-password-and-username")]
        public async Task<IActionResult> SendEmails(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email doesn't exist."
                });
            }
            var password = GeneratePassword();
            var username = user.Username;
            var firstName = user.FirstName;
            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Your username and password", EmailBody.EmailStringBody2(email, username, password, firstName));
            _emailService.SendEmail(emailModel);
            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
           
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email sent!"
            });
        }

        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //private string HashPassword(string password)
        //{
        //    var sha256 = SHA256.Create();
        //    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    return Convert.ToBase64String(hashedBytes);
        //}

            //[HttpPost("Email")]
            //public async Task<ActionResult<IFormFile>> PostAsync(IFormFile image)
            //{
            //    if (image == null)
            //    {
            //        return BadRequest("No file selected");
            //    }

            //    byte[] fileBytes;

            //    using (var ms = new MemoryStream())
            //    {
            //        image.CopyTo(ms);
            //        fileBytes = ms.ToArray();
            //    }

            //    var filePath = "./ProfilePics/image.png";

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await stream.WriteAsync(fileBytes);
            //    }

            //    //

            //    if (!System.IO.File.Exists(filePath))
            //    {
            //        return NotFound("Not found");
            //    }

            //    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //    var fileExtension = Path.GetExtension(filePath);

            //    return new FileStreamResult(fileStream, $"image/{fileExtension[1..]}");
            //}
        }
}
