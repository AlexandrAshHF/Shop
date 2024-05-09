namespace CursVN.Persistance.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte Discount { get; set; }
        public uint Number { get; set; }
        public List<string> ImageLinks { get; set; }
        public TypeEntity Type { get; set; }
        public Guid TypeId { get; set; }
        public List<ParamValues> ParamValues { get; set; }
        public List<OrderProduct> ProductsOrders { get; set; }
    }
}
