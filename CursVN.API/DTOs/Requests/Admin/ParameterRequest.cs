namespace CursVN.API.DTOs.Requests.Admin
{
    public class ParameterRequest
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<string>? AllowValues { get; set; }
        public List<Guid>? TypesId { get; set; }
    }
}
