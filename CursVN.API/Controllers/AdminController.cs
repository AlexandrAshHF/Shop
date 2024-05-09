using CursVN.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]/{secretKey}")]
    [ApiController]
    [TypeFilter<AdminFilter>]
    public class AdminController : ControllerBase
    {
    }
}
