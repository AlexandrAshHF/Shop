namespace CursVN.Core.Abstractions.AuthServices
{
    public interface IUserService
    {
        Task<ModelWrapper<string>> LogIn(string email, string password);
        Task<ModelWrapper<string>> SignUp(string email, string password);
    }
}
