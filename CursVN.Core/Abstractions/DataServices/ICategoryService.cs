using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(Guid id);
        Task<Guid> Create(Category category);
        Task<Guid> Update(Category category);
        Task Delete(Guid id);
    }
}
