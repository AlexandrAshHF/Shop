using CursVN.Core.Models;

namespace CursVN.Persistance.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProduct> ProductsOrders { get; set; }
        public decimal Amount { get; set; }
        public UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}