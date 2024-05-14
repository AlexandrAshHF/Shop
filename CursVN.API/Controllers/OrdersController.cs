using CursVN.API.DTOs.Requests.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] OrderRangeRequest request)
        {
            return Ok();
        }

        [HttpGet("GetByUser")]
        public IActionResult GetByUser([FromQuery]OrderRangeRequest request)
        {
            return Ok();
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            return Ok();
        }
    }
}
