using CursVN.Core.Abstractions.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CTPController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ITypeService _typeService;
        private IParameterService _parameterService;
        public CTPController(ICategoryService categoryService, ITypeService typeService,
            IParameterService parameterService)
        {
            _categoryService = categoryService;
            _typeService = typeService;
            _parameterService = parameterService;
        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetTypes")]
        public IActionResult GetTypes()
        {
            var result = _typeService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetTypesByCId")]
        public IActionResult GetTypesById([FromQuery]Guid id)
        {
            var result = _typeService.GetByCategoryId(id);
            return Ok(result);
        }
    }
}
