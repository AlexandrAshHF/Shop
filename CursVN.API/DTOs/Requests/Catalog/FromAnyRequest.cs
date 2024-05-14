namespace CursVN.API.DTOs.Requests.Catalog
{
    public class FromAnyRequest
    {
        public Guid id { get; set; }
        public RangeRequest? Range { get; set; }
    }
}
