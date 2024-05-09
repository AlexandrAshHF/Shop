namespace CursVN.API.DTOs.Requests.Order
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> ProductsId { get; set; }
    }
}
