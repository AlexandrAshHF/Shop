using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Application.DataServices
{
    public class OrderService : IOrderService
    {
        private ApplicationContext _context;
        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Order order)
        {
            var entity = new OrderEntity
            {
                Id = order.Id,
                UserId = order.UserId,
                DateOfCreate = DateTime.Now,
                Amount = order.Amount,
                ProductsOrders = order.Products
                .Select(x => new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    NumberOfProducts = x.NumberOfProducts,
                }).ToList(),
                Status = order.Status,
            };

            var productsId= order.Products
                .Select(x => x.ProductId);

            var products = await _context.Products
                .Where(x => productsId.Contains(x.Id))
                .ToListAsync();

            foreach (var item in order.Products)
            {
                var prodIndex = products.FindIndex(x => x.Id == item.ProductId);
                products[prodIndex].Number -= (uint)item.NumberOfProducts;
            }

            _context.UpdateRange(products);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            _context.Remove(new OrderEntity { Id = id });
            await _context.SaveChangesAsync();
        }

        public List<Order> GetAll()
        {
            var entities = _context.Orders
                .AsNoTracking()
                .Include(x => x.ProductsOrders)
                .Include(x => x.User)
                .ToList();

            return entities
                .Select(x => Order.Create(x.Id, x.DateOfCreate, x.Status,
                 x.ProductsOrders.Select(po => ProductInOrder.Create(po.ProductId, po.OrderId, po.NumberOfProducts).Model).ToList(),
                x.Amount, x.UserId
                ).Model).ToList();
        }

        public List<Order> GetByProductId(Guid productId)
        {
            var entities = _context.Orders
                .Where(x => x.ProductsOrders.Exists(x => x.ProductId == productId))
                .Include(x => x.ProductsOrders)
                .ToList();

            return entities
                .Select(x => Order.Create(x.Id, x.DateOfCreate, x.Status,
                 x.ProductsOrders.Select(po => ProductInOrder.Create(po.ProductId, po.OrderId, po.NumberOfProducts).Model).ToList(),
                x.Amount, x.UserId
                ).Model).ToList();
        }

        public List<Order> GetByUserId(Guid userId)
        {
            var entities = _context.Orders
                .Where(x => x.UserId == userId)
                .Include(x => x.ProductsOrders)
                .ToList();

            return entities
                .Select(x => Order.Create(x.Id, x.DateOfCreate, x.Status,
                 x.ProductsOrders.Select(po => ProductInOrder.Create(po.ProductId, po.OrderId, po.NumberOfProducts).Model).ToList(),
                x.Amount, x.UserId
                ).Model).ToList();
        }

        public async Task<Guid> Update(Order order)
        {
            var entity = new OrderEntity
            {
                Id = order.Id,
                UserId = order.UserId,
                DateOfCreate = DateTime.Now,
                Amount = order.Amount,
                ProductsOrders = order.Products.Select(x => new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    NumberOfProducts = x.NumberOfProducts,
                }).ToList(),
                Status = order.Status
            };

            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Order> GetById(Guid Id)
        {
            var entity = await _context.Orders
                .AsNoTracking()
                .Include(x => x.ProductsOrders)
                .FirstAsync(x => x.Id == Id);

            return Order.Create(entity.Id, entity.DateOfCreate, entity.Status,
                entity.ProductsOrders
                    .Select(x => ProductInOrder.Create(x.ProductId, x.OrderId, x.NumberOfProducts).Model).ToList(),
                entity.Amount, entity.UserId
                ).Model;
        }
    }
}