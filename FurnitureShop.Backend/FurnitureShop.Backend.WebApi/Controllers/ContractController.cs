using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContracts()
    {
        var response = await _contractService.GetAllContractsAsync();
        
        return Ok(response);
    }
    
    [HttpGet("{number:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContractByNumber([FromRoute] int number)
    {
        var response = await _contractService.GetContractByNumberAsync(number);
        
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddContract([FromBody] AddContractRequest addContractRequest)
    {
        var response = await _contractService.AddContractAsync(addContractRequest);
        
        return CreatedAtAction(nameof(GetContractByNumber), new { number = response.Number }, response);
    }
    
    [HttpPut("{number:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateContract([FromRoute] int number, [FromBody] UpdateContractRequest updateContractRequest)
    {
        await _contractService.UpdateContractAsync(number, updateContractRequest);
        
        return NoContent();
    }
    
    [HttpDelete("{number:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteContract([FromRoute] int number)
    {
        await _contractService.DeleteContractAsync(number);
        
        return NoContent();
    }
}