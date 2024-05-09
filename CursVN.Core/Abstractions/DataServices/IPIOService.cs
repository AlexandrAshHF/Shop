using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface IPIOService
    {
        List<ProductInOrder> GetAll();
        Task<ProductInOrder> GetByOrderId(Guid Id);
        Task<ProductInOrder> GetByProductId(Guid Id);
        Task<Guid> Create(ProductInOrder productInOrder);
        Task<Guid> Update(ProductInOrder productInOrder);
        Task Delete(Guid Id);
    }
}
