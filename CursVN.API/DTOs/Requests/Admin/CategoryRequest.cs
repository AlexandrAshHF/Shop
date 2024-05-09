namespace CursVN.API.DTOs.Requests.Admin
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public List<Guid> TypesId { get; set; }
    }
}
