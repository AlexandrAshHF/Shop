using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface IProductService
    {
        List<Product> GetAll();
        Task<Product> GetById(Guid id);
        List<Product> GetByCategoryId(Guid id);
        List<Product> GetByOrderId(Guid id);
        List<Product> GetByTypeId(Guid id);
        Task<Guid> Create(Product product);
        Task<Guid> Update(Product product);
        Task Delete(Guid id);
    }
}