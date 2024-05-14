using CursVN.API.DTOs.Requests.Catalog;
using CursVN.Core.Abstractions.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private IProductService _productService;
        public CatalogController(IProductService productService, ICategoryService categoryService,
            ITypeService typeService, IParameterService parameterService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetRangeFromAll")]
        public IActionResult GetRangeFromAll([FromQuery]RangeRequest request)
        {
            return Ok();
        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByCategoryId([FromQuery]FromAnyRequest request)
        {
            var result = _productService.GetByCategoryId(request.id);
            return Ok(result);
        }

        [HttpGet("GetByType")]
        public IActionResult GetByTypeId([FromQuery]FromAnyRequest request)
        {
            var result = _productService.GetByTypeId(request.id);
            return Ok(result);
        }
    }
}
