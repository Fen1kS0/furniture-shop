namespace FurnitureShop.Backend.Report.Interfaces.Services;

public interface IReportService
{
    Task<string> CreateReportFile(DateTime start, DateTime end);
}