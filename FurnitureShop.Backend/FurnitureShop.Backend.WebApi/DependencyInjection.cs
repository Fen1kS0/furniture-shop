using FurnitureShop.Backend.WebApi.Filters;

namespace FurnitureShop.Backend.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomController(this IServiceCollection services)
    {
        services.AddControllers(opt =>
            {
                opt.Filters.Add<ExceptionFilter>();
                opt.Filters.Add<ValidationFilter>();
            })
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
        
        return services;
    }

}