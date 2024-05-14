using CursVN.API.DTOs.Requests.Admin;
using CursVN.API.Filters;
using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Controllers
{
    [Route("api/[controller]/{secretKey}")]
    [ApiController]
    [TypeFilter<AdminFilter>]
    public class AdminController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ITypeService _typeService;
        private IParameterService _parameterService;
        private IProductService _productService;
        public AdminController(ICategoryService categoryService, ITypeService typeService,
            IParameterService parameterService, IProductService productService)
        {
            _categoryService = categoryService;
            _typeService = typeService;
            _parameterService = parameterService;
            _productService = productService;
        }

        [HttpPut("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            var model = Category.Create(Guid.NewGuid(), request.Name, request.TypesId ?? new List<Guid>());

            if (model.IsValid)
            {
                var id = await _categoryService.Create(model.Model);
                return Ok(id);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest request)
        {
            var model = Category.Create(Guid.Parse(request.Id), request.Name, request.TypesId ?? new List<Guid>());

            if (model.IsValid)
            {
                var id = await _categoryService.Update(model.Model);
                return Ok(id);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] Guid id)
        {
            await _categoryService.Delete(id);
            return Ok(id);
        }

        [HttpPut("CreateParameter")]
        public async Task<IActionResult> CreateParameter([FromBody] ParameterRequest request)
        {
            var model = Parameter.Create(Guid.NewGuid(), request.Name, request.TypesId ?? new List<Guid>());

            if (model.IsValid)
            {
                var result = await _parameterService.Create(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpPatch("UpdateParameter")]
        public async Task<IActionResult> UpdateParameter([FromBody] ParameterRequest request)
        {
            var model = Parameter.Create(Guid.Parse(request.Id), request.Name, request.TypesId ?? new List<Guid>());

            if (model.IsValid)
            {
                var result = await _parameterService.Update(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpDelete("DeleteParameter")]
        public async Task<IActionResult> DeleteParameter([FromBody] Guid id)
        {
            var result = await _parameterService.Delete(id);
            return Ok(result);
        }

        [HttpPut("CreateType")]
        public async Task<IActionResult> CreateType([FromBody] TypeRequest request)
        {
            var model = Core.Models.Type.Create(Guid.NewGuid(), request.ParrentId == null ? null : Guid.Parse(request.ParrentId),
                request.Name, request.ParametersId, new List<Guid>(), request.CategoryId);

            if (model.IsValid)
            {
                var result = await _typeService.Create(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpPatch("UpdateType")]
        public async Task<IActionResult> UpdateType([FromBody] TypeRequest request)
        {
            var model = Core.Models.Type.Create(Guid.Parse(request.Id), Guid.Parse(request.ParrentId),
                request.Name, request.ParametersId, new List<Guid>(), request.CategoryId);

            if (model.IsValid)
            {
                var result = await _typeService.Create(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpDelete("DeleteType")]
        public async Task<IActionResult> DeleteType([FromBody] Guid id)
        {
            await _typeService.Delete(id);
            return Ok(id);
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