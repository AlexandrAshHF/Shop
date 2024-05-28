using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.AuthServices
{
    public interface IUserService
    {
        Task<ModelWrapper<string>> LogIn(string email, string password);
        Task<ModelWrapper<string>> SignUp(string email, string password);
        Task<User> GetById(Guid id);
        Task<Guid> ConfirmUser(int code);
        List<User>GetUsers();
    }
}
