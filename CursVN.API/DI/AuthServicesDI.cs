using CursVN.Application.AuthServices;
using CursVN.Core.Abstractions.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CursVN.API.DI
{
    public static class AuthServicesDI
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opth =>
                {
                    opth.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                    };
                });

            serviceCollection.AddAuthentication();

            serviceCollection.AddScoped<IUserService, UserService>();

            return serviceCollection;
        }
    }
}
