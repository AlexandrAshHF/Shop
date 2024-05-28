namespace CursVN.Persistance.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DateOfReg { get; set; }
        public int ConfirmationCode { get; set; }
        public List<OrderEntity> Orders { get; set; }
    }
}