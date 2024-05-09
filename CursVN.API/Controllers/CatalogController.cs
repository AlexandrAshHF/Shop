using CursVN.API.DTOs.Requests.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("GetRangeFromAll")]
        public IActionResult GetRangeFromAll([FromQuery]RangeRequest request)
        {
            return Ok();
        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByCategoryId([FromQuery]FromAnyRequest request)
        {
            return Ok();
        }

        [HttpGet("GetByType")]
        public IActionResult GetByTypeId([FromQuery]FromAnyRequest request)
        {
            return Ok();
        }
    }
}
