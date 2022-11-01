using FurnitureShop.Backend.Common.DTOs.Sales;

namespace FurnitureShop.Backend.BL.Interfaces.Services;

public interface ISaleService
{
    Task<IEnumerable<SaleResponse>> GetAllSaleAsync();
    Task<IEnumerable<SaleResponse>> GetSaleByContractAsync(int contractNumber);
    Task<IEnumerable<SaleResponse>> GetSaleByFurnitureAsync(int furnitureModel);
    Task<SaleAddedResponse> AddSaleAsync(AddSaleRequest addSaleRequest);
    Task UpdateSaleAsync(int contractNumber, int furnitureModel, UpdateSaleRequest updateSaleRequest);
    Task DeleteSaleAsync(int contractNumber, int furnitureModel);
}