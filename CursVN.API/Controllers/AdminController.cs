using Azure.Core;
using CursVN.API.DTOs.Requests.Admin;
using CursVN.API.Filters;
using CursVN.Core.Abstractions.AuthServices;
using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Abstractions.Other;
using CursVN.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

/*
    Класс для админа, который отвечает за создание параметров, типов, категории, продукты
    Вход осуществляется по ключу используя AdminFilter
*/

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
        private IImageService _imageService;
        private IUserService _userService;
        public AdminController(ICategoryService categoryService, ITypeService typeService, IUserService userService,
            IParameterService parameterService, IProductService productService, IImageService imageService)
        {
            _categoryService = categoryService;
            _typeService = typeService;
            _parameterService = parameterService;
            _productService = productService;
            _imageService = imageService;
        }

        //Проверка на верность ключа админа
        [HttpPost("CheckKey")]
        public IActionResult CheckKey()
        {
            return Ok();
        }

        [HttpPut("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryRequest request)
        {
            MemoryStream ms = new MemoryStream();
            string link = string.Empty;
            if (request.Image!=null)
            {
                await request.Image.CopyToAsync(ms);
                link = await _imageService.Upload(ms);
            }
            
            var model = Category.Create(Guid.NewGuid(), request.Name, request.TypesId ?? new List<Guid>(), link);

            if (model.IsValid)
            {
                var id = await _categoryService.Create(model.Model);
                return Ok(id);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm] CategoryRequest request)
        {
            MemoryStream ms = new MemoryStream();
            string link = string.Empty;
            if (request.Image!=null)
            {
                await request.Image.CopyToAsync(ms);
                link = await _imageService.Upload(ms);
            }

            var category = await _categoryService.GetById(Guid.Parse(request.Id));

            if(!category.ImageLink.IsNullOrEmpty() && link.IsNullOrEmpty())
                link = category.ImageLink;

            var model = Category.Create(Guid.Parse(request.Id), request.Name, request.TypesId ?? new List<Guid>(), link);

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
            var model = Parameter.Create(Guid.NewGuid(), request.Name, request.AllowValues, request.TypesId ?? new List<Guid>());

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
            var model = Parameter.Create(Guid.Parse(request.Id), request.Name, request.AllowValues, request.TypesId ?? new List<Guid>());

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

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
        {
            List<string>imgLinks = new List<string>();

            if (request.Images != null)
            {
                foreach (var item in request.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        imgLinks.Add(await _imageService.Upload(ms));
                    }
                }
            }

            List<List<string>> list = new List<List<string>>();
            list.Add(request.ParamValues);

            var model = Product.Create(Guid.NewGuid(), request.Name, request.Description, request.Price,
                (byte)request.Discount, (uint)request.Number, imgLinks, request.TypeId, list);

            if(model.IsValid)
            {
                var result = await _productService.Create(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest request)
        {
            var product = await _productService.GetById(request.Id.IsNullOrEmpty() 
                ?  throw new ArgumentNullException("Id cannot be null")
                : Guid.Parse(request.Id));

            List<string> imgLinks = request.ImageLinks ?? new List<string>();

            if(request.Images != null)
            {
                foreach (var item in request.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        imgLinks.Add(await _imageService.Upload(ms));
                    }
                }
            }

            List<List<string>> list = new List<List<string>>();
            list.Add(request.ParamValues);

            var model = Product.Create(Guid.Parse(request.Id), request.Name, request.Description, request.Price,
                (byte)request.Discount, (uint)request.Number, imgLinks, request.TypeId, list);

            if (model.IsValid)
            {
                var result = await _productService.Update(model.Model);
                return Ok(result);
            }

            return BadRequest(model.ErrorMessage);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid id)
        {
            await _productService.Delete(id);
            return Ok(id);
        }
    }
}