namespace CursVN.Persistance.Entities
{
    public class OrderProduct
    {
        public OrderEntity Order { get; set; }
        public Guid OrderId { get; set; }
        public ProductEntity Product { get; set; }
        public Guid ProductId { get; set; }
        public int NumberOfProducts { get; set; }
    }
}
