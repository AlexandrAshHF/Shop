using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Application.DataServices
{
    public class CategoryService : ICategoryService
    {
        private ApplicationContext _context;
        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Category category)
        {
            if (category == null)
                throw new ArgumentException("category is null");

            var entity = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name,
                Types = await _context.Types.AsNoTracking()
                    .Where(x => category.TypesId.Contains(x.Id))
                    .ToListAsync(),
                ImageLink = category.ImageLink,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            var types = await _context.Types
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();

            foreach (var item in types)
            {
                item.CategoryId = null;
            }

            _context.Types.UpdateRange(types);

            _context.Remove(new CategoryEntity { Id = id });
            await _context.SaveChangesAsync();
        }

        public List<Category> GetAll()
        {
            var entites = _context.Categories
                .AsNoTracking()
                .Include(x => x.Types)
                .ToList();

            return entites
                .Select(x => 
                    Category.Create(x.Id, x.Name, x.Types.Select(t => t.Id)
                    .ToList(), x.ImageLink).Model)
                .ToList();
        }

        public async Task<Category> GetById(Guid id)
        {
            var entity = await _context.Categories
                .AsNoTracking()
                .Include(x => x.Types)
                .FirstAsync(X => X.Id == id);

            return Category.Create(entity.Id, entity.Name, entity.Types.Select(t => t.Id).ToList(), entity.ImageLink).Model;
        }

        public async Task<Guid> Update(Category category)
        {
            var entity = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name,
                Types = await _context.Types.AsNoTracking()
                    .Where(x => category.TypesId.Contains(x.Id))
                    .ToListAsync(),
                ImageLink = category.ImageLink,
            };

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
