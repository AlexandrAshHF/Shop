using CursVN.API.DTOs.Requests.Order;
using CursVN.API.Filters;
using CursVN.Core.Abstractions.AuthServices;
using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Abstractions.Other;
using CursVN.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

/*
    Работа с заказами - создание, обновление и просмотр
 */

namespace CursVN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        private IProductService _productService;
        private IPIOService _pioService;
        private IUserService _userService;
        private IEmailService _emailService;
        private IDocService<List<Order>> _docService;
        public OrdersController(IOrderService orderService, IProductService productService,
            IPIOService pioService, IUserService userService, IEmailService emailService,
            IDocService<List<Order>> docService)
        {
            _orderService = orderService;
            _productService = productService;
            _pioService = pioService;
            _userService = userService;
            _emailService = emailService;
            _docService = docService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            
            return Ok(result);
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId()
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
                return BadRequest("UserId is null");

            var result = _orderService.GetByUserId(Guid.Parse(userId));

            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] List<CreateOrderRequest> productsId)
        {
            var userId = User.FindFirst("userId")?.Value;
            decimal amount = 0;

            if (userId == null)
                return BadRequest("UserId is null");

            var user = await _userService.GetById(Guid.Parse(userId));

            Guid orderId = Guid.NewGuid();
            List<Tuple<Guid, int>> pio = new List<Tuple<Guid, int>>();

            for (int i = 0; i < productsId.Count; i++)
            {
                if(pio.FirstOrDefault(x => x.Item1 == productsId[i].Id) == null)
                {
                    pio.Add(new Tuple<Guid, int>(productsId[i].Id, 1));
                }
                else
                {
                    int index = pio.FindIndex(x => x.Item1 == productsId[i].Id);
                    pio[index] = new Tuple<Guid, int>(productsId[i].Id, pio[index].Item2);
                }
            }

            foreach (var item in pio)
            {
                var currentProd = await _productService.GetById(item.Item1);
                decimal disc = (decimal)currentProd.Discount / 100;
                amount += (currentProd.Price - currentProd.Price * disc) * item.Item2;
            }

            var order = Order.Create(orderId, DateTime.Now, OrderStatus.Processed,
                pio.Select(x => ProductInOrder.Create(x.Item1, orderId, x.Item2).Model).ToList(), amount, Guid.Parse(userId));

            var result = await _orderService.Create(order.Model);

            await _emailService.SendMail($"Your order was create in {order.Model.DateOfCreate} with amount {order.Model.Amount}",
                user.Email);

            return Ok(result);
        }

        [HttpPatch("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            var order = await _orderService.GetById(request.Id);
            var user = await _userService.GetById(order.UserId);

            order.Status = request.Status;

            var result = await _orderService.Update(order);

            await _emailService.SendMail($"Your order changed status: {request.Status}", user.Email);

            return Ok(result);
        }

        [HttpGet("DownloadExcel")]
        public async Task<IActionResult> CreateExcelDoc([FromQuery]CreateExcelRequest request)
        {
            var ordersFromDB = _orderService.GetAll();

            if (ordersFromDB.Count < 1)
                return BadRequest("Orders count zero");

            List<Order>orders = new List<Order>();

            foreach (var item in ordersFromDB)
            {
                if(item.DateOfCreate.Date < request.rightBorder.Date
                    && item.DateOfCreate.Date > request.leftBorder.Date)
                {
                    orders.Add(item);
                }
            }

            var result = await _docService.CreateDocument(orders);
            return File(result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Report.xlsx");
        }
    }
}
