using System.Reflection;
using FurnitureShop.Backend.Report.Interfaces.Services;
using FurnitureShop.Backend.Report.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Backend.Report;

public static class DependencyInjection
{
    public static IServiceCollection AddReportService(this IServiceCollection services)
    {
        services.AddScoped<IReportService, PdfReportService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}