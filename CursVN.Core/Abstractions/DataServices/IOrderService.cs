using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Task<Order> GetById(Guid Id);
        List<Order> GetByUserId(Guid userId);
        List<Order> GetByProductId(Guid productId);
        Task<Guid> Create(Order order);
        Task<Guid> Update(Order order);
        Task Delete(Guid id);
    }
}
