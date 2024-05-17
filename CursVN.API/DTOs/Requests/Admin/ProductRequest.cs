namespace CursVN.API.DTOs.Requests.Admin
{
    public class ProductRequest
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Number { get; set; }
        public List<IFormFile> ImageLinks { get; set; }
        public Guid TypeId { get; set; }
        public List<string> ParamValues { get; set; }
    }
}
