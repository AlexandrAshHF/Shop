using CursVN.API.DTOs.Requests.Account;
using CursVN.API.Filters;
using CursVN.Core.Abstractions.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserService _userService;
        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]AccountRequest request)
        {
            var result = await _userService.LogIn(request.Email, request.Password);

            if (result.IsValid)
            {
                string response = "Bearer " + result.Model;
                return Ok(response);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]AccountRequest request)
        {
            var result = await _userService.SignUp(request.Email, request.Password);

            if (result.IsValid)
            {
                string response = "Bearer " + result.Model;
                return Ok(response);
            }

            return BadRequest(result.ErrorMessage);
        }

        [TypeFilter<AdminFilter>]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var result = _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet("GetUserById")]
        [Authorize]
        public IActionResult GetAuthorizeUser()
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
                return BadRequest("UserId is null");

            var user = _userService.GetById(Guid.Parse(userId));

            return Ok(user);
        }
    }
}