using crm_software_back.Models;
using crm_software_back.Services.UserServices;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPost("Email")]
        public async Task<ActionResult<IFormFile>> PostAsync(IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("No file selected");
            }

            byte[] fileBytes;
            
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            var filePath = "./ProfilePics/image.png";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(fileBytes);
            }

            //

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Not found");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileExtension = Path.GetExtension(filePath);

            return new FileStreamResult(fileStream, $"image/{fileExtension[1..]}");
        }
    }
}
