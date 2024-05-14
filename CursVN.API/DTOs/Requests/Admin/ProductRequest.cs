namespace CursVN.API.DTOs.Requests.Admin
{
    public class ProductRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte? Discount { get; set; }
        public uint Number { get; set; }
        public List<IFormFile> ImageLinks { get; set; }
        public Guid TypeId { get; set; }
        public List<List<string>> ParamValues { get; set; }
    }
}
