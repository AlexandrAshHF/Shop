using CursVN.API.DTOs.Requests.Catalog;

namespace CursVN.API.DTOs.Requests.Order
{
    public class OrderRangeRequest
    {
        public Guid Id { get; set; }
        public RangeRequest Range { get; set; }
    }
}
