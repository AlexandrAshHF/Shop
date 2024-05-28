using System.Collections.Generic;

namespace CursVN.Core.Models
{
    public class User
    {
        private User(Guid id, string email, string passwordHash, 
            DateTime dateOfReg, bool confirm, List<Guid> ordersId, int confirmationCode)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            IsConfirmed = confirm;
            DateOfReg = dateOfReg;
            OrdersId = ordersId;
            ConfirmationCode = confirmationCode;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DateOfReg { get; set; }
        public int ConfirmationCode { get; set; }
        public List<Guid> OrdersId { get; set; }
        public static ModelWrapper<User>Create(Guid id, string email, string passwordHash,
            DateTime dateOfReg, bool confirm, List<Guid> ordersId, int confirmCode)
        {
            return new ModelWrapper<User>(
                model: new User(id, email, passwordHash, dateOfReg, confirm, ordersId, confirmCode),
                error: string.Empty,
                valid: true
                );
        }
    }
}
