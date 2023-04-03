using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.LoginUserServices;
using crm_software_back.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private readonly ILoginUserService _loginUserService;

        public LoginUserController(ILoginUserService loginUserService)
        {
            _loginUserService = loginUserService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<LoginUser?>> getLoginUser(int userId)
        {
            var loginUser = await _loginUserService.getLoginUser(userId);

            if (loginUser == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(loginUser);
        }

        [HttpPost]
        public async Task<ActionResult<LoginUser?>> postLoginUser(DTOUser newLoginUser)
        {
            var loginUser = await _loginUserService.postLoginUser(newLoginUser);

            if (loginUser == null)
            {
                return NotFound("User is already exist..!");
            }

            return Ok(loginUser);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<LoginUser?>> putLoginUser(int userId, DTOUser newLoginUser)
        {
            var loginUser = await _loginUserService.putLoginUser(userId, newLoginUser);

            if (loginUser == null)
            {
                return NotFound("User does not exist..!");
            }

            return Ok(loginUser);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<LoginUser?>> deleteLoginUser(int userId)
        {
            var loginUser = await _loginUserService.deleteLoginUser(userId);

            if (loginUser == null)
            {
                return NotFound("User is not found..!");
            }

            return Ok(loginUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(DTOUser request)
        {
            var result = await _loginUserService.login(request);

            if (result == null)
            {
                return BadRequest("Username or Password is Incorrect...!");
            }

            return result;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<DTOLoginUser>> GetTokenData()
        {
            var user = await _loginUserService.getTokenData();

            return Ok(user);
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<bool>> authenticateUser(DTOUser request)
        {
            return await _loginUserService.authenticateUser(request);
        }
    }
}
