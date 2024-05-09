using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CursVN.API.Filters
{
    public class AdminFilter : IAuthorizationFilter
    {
        private IConfiguration _configuration;
        public AdminFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var secretkey = _configuration["AdminKey"];
            var secretKeyFromRoute = (string)context.RouteData.Values["secretKey"];

            if (secretkey != secretKeyFromRoute)
                context.Result = new UnauthorizedResult();
        }
    }
}
