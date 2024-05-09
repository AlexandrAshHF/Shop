using CursVN.Core.Abstractions.DataServices;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Application.DataServices
{
    public class TypeService : ITypeService
    {
        private ApplicationContext _context;
        public TypeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Core.Models.Type type)
        {
            var entity = new TypeEntity
            {
                Id = type.Id,
                CategoryId = type.CategoryId,
                Name = type.Name,
                Parameters = type.ParametersId
                        .Select(x => new ParameterEntity { Id = x })
                        .ToList(),
                ParrentId = type.ParrentId,
                Products = type.ProductsId
                        .Select(x => new ProductEntity { Id = x })
                        .ToList()
            };

            await _context.Types.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            _context.Types.Remove(new TypeEntity { Id = id });
            await _context.SaveChangesAsync();
        }

        public List<Core.Models.Type> GetAll()
        {
            var entities = _context.Types
                .AsNoTracking()
                .Include(x => x.Parameters)
                .Include(x => x.Products)
                .ToList();

            return entities.Select(x => Core.Models.Type.Create(
                    x.Id, x.ParrentId, x.Name, x.Parameters.Select(x => x.Id).ToList(),
                    x.Products.Select(x => x.Id).ToList(), x.CategoryId
                ).Model).ToList();
        }

        public List<Core.Models.Type> GetByCategoryId(Guid id)
        {
            var entities = _context.Types
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .Include(x => x.Parameters)
                .Include(x => x.Products)
                .ToList();

            return entities.Select(x => Core.Models.Type.Create(
                    x.Id, x.ParrentId, x.Name, x.Parameters.Select(x => x.Id).ToList(),
                    x.Products.Select(x => x.Id).ToList(), x.CategoryId
                ).Model).ToList();
        }

        public async Task<Core.Models.Type> GetById(Guid Id)
        {
            var entity = await _context.Types
                .AsNoTracking()
                .Include(x => x.Parameters)
                .Include(x => x.Products)
                .FirstAsync(x => x.Id == Id);

            return Core.Models.Type.Create(
                    entity.Id, entity.ParrentId, entity.Name, entity.Parameters.Select(x => x.Id).ToList(),
                    entity.Products.Select(x => x.Id).ToList(), entity.CategoryId
                ).Model;
        }

        public List<Core.Models.Type> GetByParrentId(Guid id)
        {
            var entities = _context.Types
                .AsNoTracking()
                .Where(x => x.ParrentId == id)
                .Include(x => x.Parameters)
                .Include(x => x.Products)
                .ToList();

            return entities.Select(x => Core.Models.Type.Create(
                    x.Id, x.ParrentId, x.Name, x.Parameters.Select(x => x.Id).ToList(),
                    x.Products.Select(x => x.Id).ToList(), x.CategoryId
                ).Model).ToList();
        }

        public async Task<Guid> Update(Core.Models.Type type)
        {
            var entity = new TypeEntity
            {
                Id = type.Id,
                CategoryId = type.CategoryId,
                Name = type.Name,
                Parameters = type.ParametersId
                        .Select(x => new ParameterEntity { Id = x })
                        .ToList(),
                ParrentId = type.ParrentId,
                Products = type.ProductsId
                        .Select(x => new ProductEntity { Id = x })
                        .ToList()
            };

            _context.Types.Update(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
