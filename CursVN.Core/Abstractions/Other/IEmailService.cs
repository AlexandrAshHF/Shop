namespace CursVN.Core.Abstractions.Other
{
    public interface IEmailService
    {
        Task SendMail(string message, string consumerEmail);
    }
}
