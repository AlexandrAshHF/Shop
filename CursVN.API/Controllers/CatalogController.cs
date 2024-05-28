using CursVN.API.DTOs.Requests.Catalog;
using CursVN.Core.Abstractions.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*
    Получение продуктов из БД, слово "By" озгначет получение продуктов из БД по параметру, который указан после "By"
 */

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

/*        [HttpGet("GetRangeFromAll")]
        public IActionResult GetRangeFromAll([FromQuery]RangeRequest request)
        {
            var result = _productService.GetRange(request.Page, request.Limit);
            return Ok();
        }*/

        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetByCategoryId([FromQuery]FromAnyRequest request)
        {
            var result = await _productService.GetByCategoryId(request.id, request?.Range?.Page, request?.Range?.Limit);
            return Ok(result);
        }

        [HttpGet("GetByType")]
        public async Task<IActionResult> GetByTypeId([FromQuery]FromAnyRequest request)
        {
            var result = await _productService.GetByTypeId(request.id, request?.Range?.Page, request?.Range?.Limit);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }
    }
}
