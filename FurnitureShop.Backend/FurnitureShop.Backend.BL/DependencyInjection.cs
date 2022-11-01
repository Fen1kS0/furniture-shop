using System.Reflection;
using FluentValidation;
using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Backend.BL;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBuyerService, BuyerService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IFurnitureService, FurnitureService>();
        services.AddScoped<ISaleService, SaleService>();

        return services;
    }

    
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

}