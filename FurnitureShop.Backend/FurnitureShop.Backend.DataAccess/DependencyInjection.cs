using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Backend.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOpt => sqlOpt.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
        return services;
    }

}