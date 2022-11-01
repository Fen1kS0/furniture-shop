using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.DTOs.Buyers;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController : ControllerBase
{
    private readonly IBuyerService _buyerService;

    public BuyerController(IBuyerService buyerService)
    {
        _buyerService = buyerService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBuyers()
    {
        var response = await _buyerService.GetAllBuyersAsync();
        
        return Ok(response);
    }
    
    [HttpGet("{code:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuyerByCode([FromRoute] int code)
    {
        var response = await _buyerService.GetBuyerByCodeAsync(code);
        
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddBuyer([FromBody] AddBuyerRequest addBuyerRequest)
    {
        var response = await _buyerService.AddBuyerAsync(addBuyerRequest);
        
        return CreatedAtAction(nameof(GetBuyerByCode), new { code = response.Code }, response);
    }
    
    [HttpPut("{code:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBuyer([FromRoute] int code, [FromBody] UpdateBuyerRequest updateBuyerRequest)
    {
        await _buyerService.UpdateBuyerAsync(code, updateBuyerRequest);
        
        return NoContent();
    }
    
    [HttpDelete("{code:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBuyer([FromRoute] int code)
    {
        await _buyerService.DeleteBuyerAsync(code);
        
        return NoContent();
    }
}