using CursVN.Core.Models;

namespace CursVN.API.DTOs.Requests.Order
{
    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
