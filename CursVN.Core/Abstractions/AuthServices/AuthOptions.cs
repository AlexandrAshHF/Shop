using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CursVN.Core.Abstractions.AuthServices
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        public const int ExpiresHours = 6;
        public const string Key = "m312ysupers2131ecret_secretse312c211retsecretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
