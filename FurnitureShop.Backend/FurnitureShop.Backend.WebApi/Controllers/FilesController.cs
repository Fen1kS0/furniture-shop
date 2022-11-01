using FurnitureShop.Backend.Report.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IReportService _reportService;

    public FilesController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("report")]
    public async Task<IActionResult> DownlandReport([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var path = await _reportService.CreateReportFile(start.ToLocalTime(), end.ToLocalTime());
        var stream = new FileStream(path, FileMode.Open);
        return new FileStreamResult(stream, "application/pdf");
    }
}