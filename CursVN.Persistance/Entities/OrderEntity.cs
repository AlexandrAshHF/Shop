namespace CursVN.Persistance.Entities
{
    public enum OrderStatus
    {
        None = 0,
        Processed = 1,
        Sent = 2,
        Received = 3,
    }
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProduct> ProductsOrders { get; set; }
        public UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}