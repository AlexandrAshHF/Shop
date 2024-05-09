using CursVN.API.DTOs.Requests.Admin;
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
        [HttpPut("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            return Ok();
        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest request)
        {
            return Ok();
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] Guid id)
        {
            return Ok();
        }

        [HttpPut("CreateParameter")]
        public async Task<IActionResult> CreateParameter([FromBody] ParameterRequest request)
        {
            return Ok();
        }

        [HttpPatch("UpdateParameter")]
        public async Task<IActionResult> UpdateParameter([FromBody] ParameterRequest request)
        {
            return Ok();
        }

        [HttpDelete("DeleteParameter")]
        public async Task<IActionResult> DeleteParameter([FromBody] Guid id)
        {
            return Ok();
        }

        [HttpPut("CreateType")]
        public async Task<IActionResult> CreateType([FromBody] TypeRequest request)
        {
            return Ok();
        }

        [HttpPatch("UpdateType")]
        public async Task<IActionResult> UpdateType([FromBody] TypeRequest request)
        {
            return Ok();
        }

        [HttpDelete("DeleteType")]
        public async Task<IActionResult> DeleteType([FromBody] Guid id)
        {
            return Ok();
        }

        [HttpPut("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            return Ok();
        }

        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct()
        {
            return Ok();
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid id)
        {
            return Ok();
        }
    }
}