using FurnitureShop.Backend.Common.DTOs.Furniture;

namespace FurnitureShop.Backend.BL.Interfaces.Services;

public interface IFurnitureService
{
    Task<IEnumerable<FurnitureResponse>> GetAllFurnitureAsync();
    Task<FurnitureResponse> GetFurnitureByModelAsync(int model);
    Task<FurnitureAddedResponse> AddFurnitureAsync(AddFurnitureRequest addFurnitureRequest);
    Task UpdateFurnitureAsync(int model, UpdateFurnitureRequest updateFurnitureRequest);
    Task DeleteFurnitureAsync(int model);
}