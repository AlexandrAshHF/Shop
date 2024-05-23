using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetRange(int page, int limit);
        Task<Product> GetById(Guid id);
        Task<List<Product>> GetByCategoryId(Guid id, int? page, int? limit);
        List<Product> GetByOrderId(Guid id);
        Task<List<Product>> GetByTypeId(Guid id, int? page, int? limit);
        Task<Guid> Create(Product product);
        Task<Guid> Update(Product product);
        Task Delete(Guid id);
    }
}