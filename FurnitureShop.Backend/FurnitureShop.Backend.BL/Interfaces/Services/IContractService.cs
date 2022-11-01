using FurnitureShop.Backend.Common.DTOs.Contracts;

namespace FurnitureShop.Backend.BL.Interfaces.Services;

public interface IContractService
{
    Task<IEnumerable<ContractResponse>> GetAllContractsAsync();
    Task<ContractResponse> GetContractByNumberAsync(int number);
    Task<ContractAddedResponse> AddContractAsync(AddContractRequest addContractRequest);
    Task UpdateContractAsync(int number, UpdateContractRequest updateContractRequest);
    Task DeleteContractAsync(int number);
}