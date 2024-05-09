using CursVN.API.DTOs.Requests.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]AccountRequest request)
        {
            return Ok();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]AccountRequest request)
        {
            return Ok();
        }
    }
}
