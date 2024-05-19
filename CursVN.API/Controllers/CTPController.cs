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

        [HttpGet("GetParameters")]
        public IActionResult GetParameters()
        {
            var result = _parameterService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetTypeById")]
        public async Task<IActionResult> GetTypeById(Guid id)
        {
            var result = await _typeService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetParameterById")]
        public async Task<IActionResult> GetParameterById(Guid id) 
        {
            var result = await _parameterService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }
    }
}