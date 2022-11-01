using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.DTOs.Furniture;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FurnitureController : ControllerBase
{
    private readonly IFurnitureService _furnitureService;

    public FurnitureController(IFurnitureService furnitureService)
    {
        _furnitureService = furnitureService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFurniture()
    {
        var response = await _furnitureService.GetAllFurnitureAsync();
        
        return Ok(response);
    }
    
    [HttpGet("{model:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFurnitureByNumber([FromRoute] int model)
    {
        var response = await _furnitureService.GetFurnitureByModelAsync(model);
        
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddFurniture([FromBody] AddFurnitureRequest addFurnitureRequest)
    {
        var response = await _furnitureService.AddFurnitureAsync(addFurnitureRequest);
        
        return CreatedAtAction(nameof(GetFurnitureByNumber), new { model = response.Model }, response);
    }
    
    [HttpPut("{model:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateFurniture([FromRoute] int model, [FromBody] UpdateFurnitureRequest updateFurnitureRequest)
    {
        await _furnitureService.UpdateFurnitureAsync(model, updateFurnitureRequest);
        
        return NoContent();
    }
    
    [HttpDelete("{model:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteFurniture([FromRoute] int model)
    {
        await _furnitureService.DeleteFurnitureAsync(model);
        
        return NoContent();
    }
}