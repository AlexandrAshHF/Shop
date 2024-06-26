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
            var entity = await _context.Products
                .Include(x => x.ParamValues)
                .SingleAsync(x => x.Id == id);

            entity.ParamValues = new List<ParamValues>();

            _context.Products.Remove(entity);
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
        public List<Product> GetRange(int page, int limit)
        {
            var entities = _context.Products
                .Include(x => x.ParamValues)
                .ToList();

            return entities
                .GetRange(page * limit, limit)
                .Select(x => Product.Create
                    (
                        id: x.Id,
                        name: x.Name,
                        description: x.Description,
                        price: x.Price,
                        discount: x.Discount,
                        number: x.Number,
                        imageLinks: x.ImageLinks,
                        typeId: x.TypeId,
                        paramValues: x.ParamValues.Select(x => x.Values).ToList()
                    ).Model).ToList();
        }

        public async Task<List<Product>> GetByCategoryId(Guid id, int? page, int? limit)
        {
            var typesId = await _context.Types
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .Select(X => X.Id)
                .ToListAsync();

            var entities = await _context.Products
                .AsNoTracking()
                .Include(x=> x.ParamValues)
                .Where(x => typesId.Contains(x.TypeId))
                .ToListAsync();

            if (page != null && limit != null)
                entities = entities.GetRange((int)(page * limit), (int)limit);

            return entities.Select(
                    x => Product.Create(x.Id, x.Name, x.Description, x.Price, x.Discount, x.Number,
                    x.ImageLinks, x.TypeId, x.ParamValues.Select(pv => pv.Values).ToList()
                    ).Model).ToList();
        }

        public async Task<Product> GetById(Guid id)
        {
            var entity = await _context.Products
                .AsNoTracking()
                .Include(x => x.ParamValues).AsNoTracking()
                .SingleAsync(x => x.Id == id);

            List<List<string>> list = new List<List<string>>();
            foreach (var item in entity.ParamValues)
            {
                list.Add(item.Values);
            }

            return Product.Create(entity.Id, entity.Name, entity.Description, entity.Price, entity.Discount,
                entity.Number, entity.ImageLinks, entity.TypeId, list).Model;
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

        public async Task<List<Product>> GetByTypeId(Guid id, int? page, int? limit)
        {
            var entities = await _context.Products
                .AsNoTracking()
                .Include(x => x.ParamValues)
                .Where(x => x.TypeId == id)
                .ToListAsync();

            if (page != null && limit != null)
                entities = entities.GetRange((int)(page * limit), (int)limit);

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
                ParamValues = await _context.ParamValues
                    .AsNoTracking()
                    .Where(x => x.ProductEntityId == product.Id)
                    .ToListAsync(),
                Price = product.Price,
                TypeId = product.TypeId,
            };

            for(int i = 0; i < entity.ParamValues.Count; i++)
            {
                entity.ParamValues[i].Values = product.ParamValues[i];
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}