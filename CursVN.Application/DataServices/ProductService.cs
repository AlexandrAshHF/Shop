﻿using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Application.DataServices
{
    public class ProductService : IProductService
    {
        private ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Product product)
        {
            var entity = new ProductEntity
            {
                Id = product.Id,
                Description = product.Description,
                Discount = product.Discount,
                Name = product.Name,
                ImageLinks = product.ImageLinks,
                Number = product.Number,
                ParamValues = product.ParamValues
                    .Select(x => new ParamValues
                    {
                        Id = Guid.NewGuid(),
                        Values = x
                    }).ToList(),
                Price = product.Price,
                TypeId = product.TypeId,
            };
            
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task Delete(Guid id)
        {
            _context.Products.Remove(new ProductEntity { Id = id });
            await _context.SaveChangesAsync();
        }

        public List<Product> GetAll()
        {
            var entities = _context.Products
                .AsNoTracking()
                .Include(x => x.ParamValues)
                .ToList();

            return entities.Select(
                    x => Product.Create(x.Id, x.Name, x.Description, x.Price, x.Discount, x.Number,
                    x.ImageLinks, x.TypeId, x.ParamValues.Select(pv => pv.Values).ToList()
                    ).Model).ToList();
        }

        public List<Product> GetByCategoryId(Guid id)
        {
            var entities = _context.Products
                .AsNoTracking()
                .Include(x => x.ParamValues)
                .Where(x => x.TypeId == id)
                .ToList();

            return entities.Select(
                    x => Product.Create(x.Id, x.Name, x.Description, x.Price, x.Discount, x.Number,
                    x.ImageLinks, x.TypeId, x.ParamValues.Select(pv => pv.Values).ToList()
                    ).Model).ToList();
        }

        public Task<Product> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByOrderId(Guid id)
        {
            var productsId = _context.OrdersProducts
                .AsNoTracking()
                .Where(x => x.OrderId == id)
                .Select(x => x.ProductId)
                .ToList();

            var entities = productsId
                .Select(x => _context.Products.Find(x))
                .ToList();
                

            return entities.Select(
                    x => Product.Create(x.Id, x.Name, x.Description, x.Price, x.Discount, x.Number,
                    x.ImageLinks, x.TypeId, x.ParamValues.Select(pv => pv.Values).ToList()
                    ).Model).ToList();
        }

        public List<Product> GetByTypeId(Guid id)
        {
            var entities = _context.Products
                .AsNoTracking()
                .Include(x => x.ParamValues)
                .Where(x => x.TypeId == id)
                .ToList();

            return entities.Select(
                    x => Product.Create(x.Id, x.Name, x.Description, x.Price, x.Discount, x.Number,
                    x.ImageLinks, x.TypeId, x.ParamValues.Select(pv => pv.Values).ToList()
                    ).Model).ToList();
        }

        public async Task<Guid> Update(Product product)
        {
            var entity = new ProductEntity
            {
                Id = product.Id,
                Description = product.Description,
                Discount = product.Discount,
                Name = product.Name,
                ImageLinks = product.ImageLinks,
                Number = product.Number,
                ParamValues = product.ParamValues
                               .Select(x => new ParamValues
                               {
                                   Id = Guid.NewGuid(),
                                   Values = x
                               }).ToList(),
                Price = product.Price,
                TypeId = product.TypeId,
            };

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}