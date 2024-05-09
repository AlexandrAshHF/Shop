namespace CursVN.API.DTOs.Requests.Admin
{
    public class TypeRequest
    {
        public Guid Id { get; set; }
        public Guid? ParrentId { get; set; }
        public string Name { get; set; }
        public List<Guid> ParametersId { get; set; }
    }
}
