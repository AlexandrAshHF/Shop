using System.Collections.Generic;

namespace CursVN.Core.Models
{
    public class User
    {
        private User(Guid id, string email, string passwordHash, List<Guid> ordersId)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            OrdersId = ordersId;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Guid> OrdersId { get; set; }
        public static ModelWrapper<User>Create(Guid id, string email, string passwordHash, List<Guid> ordersId)
        {
            return new ModelWrapper<User>(
                model: new User(id, email, passwordHash, ordersId),
                error: string.Empty,
                valid: true
                );
        }
    }
}
