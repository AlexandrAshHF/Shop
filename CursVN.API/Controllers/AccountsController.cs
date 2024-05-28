using CursVN.API.DTOs.Requests.Account;
using CursVN.API.Filters;
using CursVN.Core.Abstractions.AuthServices;
using CursVN.Core.Abstractions.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;

/*
    Контроллер аккаунта отвечает за вход, регистрацию, отправления кода на почту
 */

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;
        public AccountsController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
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
                string response = result.Model;
                await _emailService.SendMail($"Your invite code: {response}", request.Email);
                return Ok(response);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("Confirmation")] //подтверждение аккаунта с помощью кода
        public async Task<IActionResult> ConfirmUser([FromBody] int confCode)
        {
            var result = await _userService.ConfirmUser(confCode);

            var user = await _userService.GetById(result);
            await _emailService.SendMail("Account activated successfully", user.Email);

            return Ok(result);
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