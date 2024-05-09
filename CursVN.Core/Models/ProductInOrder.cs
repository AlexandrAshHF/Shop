namespace CursVN.Core.Models
{
    public class ProductInOrder
    {
        private ProductInOrder(Guid productId, Guid orderId, int number)
        {
            ProductId = productId;
            OrderId = orderId;
            NumberOfProducts = number;
        }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int NumberOfProducts { get; set; }
        public static ModelWrapper<ProductInOrder> Create(Guid productId, Guid orderId, int number)
        {
            return new ModelWrapper<ProductInOrder>(
                model: new ProductInOrder(productId, orderId, number),
                error: string.Empty,
                valid: true
                );
        }
    }
}
