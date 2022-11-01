using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.DTOs.Sales;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSales()
    {
        var response = await _saleService.GetAllSaleAsync();
        
        return Ok(response);
    }
    
    [HttpGet("contract/{contractNumber:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSaleByContract([FromRoute] int contractNumber)
    {
        var response = await _saleService.GetSaleByContractAsync(contractNumber);
        
        return Ok(response);
    }
    
    [HttpGet("furniture/{furnitureModel:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSaleByFurniture([FromRoute] int furnitureModel)
    {
        var response = await _saleService.GetSaleByFurnitureAsync(furnitureModel);
        
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSale([FromBody] AddSaleRequest addSaleRequest)
    {
        var response = await _saleService.AddSaleAsync(addSaleRequest);
        
        return Ok(response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateSale( 
        [FromQuery] int contractNumber, 
        [FromQuery] int furnitureModel, 
        [FromBody] UpdateSaleRequest updateFurnitureRequest
        )
    {
        await _saleService.UpdateSaleAsync(contractNumber, furnitureModel, updateFurnitureRequest);
        
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteSale([FromQuery] int contractNumber, [FromQuery] int furnitureModel)
    {
        await _saleService.DeleteSaleAsync(contractNumber, furnitureModel);
        
        return NoContent();
    }
}