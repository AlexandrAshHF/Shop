using CursVN.Application.DataServices;
using CursVN.Core.Abstractions.DataServices;

namespace CursVN.API.DI
{
    public static class DataServicesDI
    {
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<IParameterService, ParameterService>();
            serviceCollection.AddScoped<IPIOService, PIOService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<ITypeService, TypeService>();

            return serviceCollection;
        }
    }
}
