using CursVN.Core.Abstractions.DataServices;
using CursVN.Core.Models;
using CursVN.Persistance;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Application.DataServices
{
    public class ParameterService : IParameterService
    {
        private ApplicationContext _context;
        public ParameterService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Parameter parameter)
        {
            var entity = new ParameterEntity
            {
                Id = parameter.Id,
                Name = parameter.Name,
                Types = await _context.Types
                    .Where(x => parameter.TypesId.Contains(x.Id))
                    .ToListAsync()
            };

            await _context.Parameters.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            _context.Parameters.Remove(new ParameterEntity { Id = id });
            await _context.SaveChangesAsync();

            return id;
        }

        public List<Parameter> GetAll()
        {
            var entities = _context.Parameters
                .AsNoTracking()
                .Include(x => x.Types)
                .ToList();

            return entities.Select(x => Parameter.Create(x.Id, x.Name,
                x.Types.Select(t => t.Id).ToList())
            .Model).ToList();
        }

        public async Task<Parameter> GetById(Guid id)
        {
            var entity = await _context.Parameters
                .AsNoTracking()
                .Include(x => x.Types)
                .FirstAsync(x => x.Id == id);

            return Parameter.Create(entity.Id, entity.Name, entity.Types
                .Select(x => x.Id).ToList()).Model;
        }

        public async Task<Guid> Update(Parameter parameter)
        {
            var entity = await _context.Parameters
                .Include(p => p.Types)
                .SingleAsync(p => p.Id == parameter.Id);

            entity.Types.Clear();

            var types = await _context.Types
                .Where(x => parameter.TypesId.Contains(x.Id))
                .ToListAsync();

            foreach (var type in types)
            {
                entity.Types.Add(type);
            }

            entity.Name = parameter.Name;
            _context.Parameters.Update(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
