namespace CursVN.API.DTOs.Requests.Admin
{
    public class CategoryRequest
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public List<Guid>? TypesId { get; set; }
    }
}
