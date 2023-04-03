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

        public UserController(IUserService userService, DataContext dataContext, IConfiguration configuration)
        {
            _userService = userService;
            _dataContext = dataContext;
            _configuration = configuration;
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

        [HttpGet("Dashboard")]
        public async Task<ActionResult<DTODashBoard>> getDashboardData()
        {
            var dashboardData = await _userService.getDashboardData();

            if (dashboardData == null)
            {
                return NotFound("Users list is Empty..!");
            }

            return Ok(dashboardData);
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
