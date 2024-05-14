namespace CursVN.API.DTOs.Requests.Admin
{
    public class TypeRequest
    {
        public string? Id { get; set; }
        public string? ParrentId { get; set; }
        public string Name { get; set; }
        public List<Guid> ParametersId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
