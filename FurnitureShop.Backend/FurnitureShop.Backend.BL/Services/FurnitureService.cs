using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Common.DTOs.Furniture;
using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.Exceptions;
using FurnitureShop.Backend.Common.Resources;
using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Backend.BL.Services;

public class FurnitureService : IFurnitureService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public FurnitureService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FurnitureResponse>> GetAllFurnitureAsync()
    {
        var furniture = await _dataContext.Furniture
            .AsSingleQuery()
            .AsNoTracking()
            .ProjectTo<FurnitureResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return furniture;
    }

    public async Task<FurnitureResponse> GetFurnitureByModelAsync(int model)
    {
        var furniture = await _dataContext.Furniture
            .AsSingleQuery()
            .AsNoTracking()
            .Where(f => f.Model == model)
            .ProjectTo<FurnitureResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (furniture == null)
        {
            throw new NotFoundException(ExceptionMessageResource.FurnitureNotFound);
        }

        return furniture;
    }

    public async Task<FurnitureAddedResponse> AddFurnitureAsync(AddFurnitureRequest addFurnitureRequest)
    {
        var furniture = _mapper.Map<Furniture>(addFurnitureRequest);

        var result = await _dataContext.Furniture.AddAsync(furniture);
        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<FurnitureAddedResponse>(result.Entity);
    }

    public async Task UpdateFurnitureAsync(int model, UpdateFurnitureRequest updateFurnitureRequest)
    {
        var furniture = await _dataContext.Furniture.FindAsync(model);
        
        if (furniture == null)
        {
            throw new NotFoundException(ExceptionMessageResource.FurnitureNotFound);
        }
        
        _mapper.Map(updateFurnitureRequest, furniture);

        _dataContext.Furniture.Update(furniture);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteFurnitureAsync(int model)
    {
        var furniture = await _dataContext.Furniture.FindAsync(model);
        
        if (furniture == null)
        {
            throw new NotFoundException(ExceptionMessageResource.FurnitureNotFound);
        }

        _dataContext.Furniture.Remove(furniture);
        await _dataContext.SaveChangesAsync();
    }
}