using FurnitureShop.Backend.Common.DTOs.Buyers;

namespace FurnitureShop.Backend.BL.Interfaces.Services;

public interface IBuyerService
{
    Task<IEnumerable<BuyerResponse>> GetAllBuyersAsync();
    Task<BuyerResponse> GetBuyerByCodeAsync(int code);
    Task<BuyerAddedResponse> AddBuyerAsync(AddBuyerRequest addBuyerRequest);
    Task UpdateBuyerAsync(int code, UpdateBuyerRequest updateBuyerRequest);
    Task DeleteBuyerAsync(int code);
}