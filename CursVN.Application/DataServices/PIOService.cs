using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CursVN.Application.DataServices
{
    public class PIOService : IPIOService
    {
        private ApplicationContext _context;
        public PIOService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(ProductInOrder productInOrder)
        {
            var entity = new OrderProduct
            {
                OrderId = productInOrder.OrderId,
                ProductId = productInOrder.ProductId,
                NumberOfProducts = productInOrder.NumberOfProducts,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return productInOrder.OrderId;
        }
        public async Task Delete(Guid Id)
        {
            var entity = await _context.OrdersProducts
                .AsNoTracking()
                .FirstAsync(x => x.OrderId == Id || x.ProductId == Id);

            _context.OrdersProducts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public List<ProductInOrder> GetAll()
        {
            var entities = _context.OrdersProducts
                .AsNoTracking()
                .ToList();

            return entities
                .Select(x => ProductInOrder.Create(x.ProductId, x.OrderId, x.NumberOfProducts).Model)
                .ToList();
        }

        public async Task<ProductInOrder> GetByOrderId(Guid Id)
        {
            var entity = await _context.OrdersProducts
                .AsNoTracking()
                .FirstAsync(x => x.OrderId == Id);

            return ProductInOrder.Create(entity.ProductId, entity.OrderId, entity.NumberOfProducts).Model;
        }

        public async Task<ProductInOrder> GetByProductId(Guid Id)
        {
            var entity = await _context.OrdersProducts
                .AsNoTracking()
                .FirstAsync(x => x.ProductId == Id);

            return ProductInOrder.Create(entity.ProductId, entity.OrderId, entity.NumberOfProducts).Model;
        }

        public async Task<Guid> Update(ProductInOrder productInOrder)
        {
            var entity = new OrderProduct
            {
                OrderId = productInOrder.OrderId,
                ProductId = productInOrder.ProductId,
                NumberOfProducts = productInOrder.NumberOfProducts,
            };

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return productInOrder.OrderId;
        }
    }
}
