using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Common.DTOs.Buyers;
using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.Exceptions;
using FurnitureShop.Backend.Common.Resources;
using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Backend.BL.Services;

public class BuyerService : IBuyerService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public BuyerService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BuyerResponse>> GetAllBuyersAsync()
    {
        var buyers = await _dataContext.Buyers
            .AsSingleQuery()
            .AsNoTracking()
            .ProjectTo<BuyerResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return buyers;
    }

    public async Task<BuyerResponse> GetBuyerByCodeAsync(int code)
    {
        var buyer = await _dataContext.Buyers
            .AsSingleQuery()
            .AsNoTracking()
            .Where(b => b.Code == code)
            .ProjectTo<BuyerResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (buyer == null)
        {
            throw new NotFoundException(ExceptionMessageResource.BuyerNotFound);
        }
        
        return buyer;
    }

    public async Task<BuyerAddedResponse> AddBuyerAsync(AddBuyerRequest addBuyerRequest)
    {
        var buyer = _mapper.Map<Buyer>(addBuyerRequest);

        var result = await _dataContext.Buyers.AddAsync(buyer);
        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<BuyerAddedResponse>(result.Entity);
    }

    public async Task UpdateBuyerAsync(int code, UpdateBuyerRequest updateBuyerRequest)
    {
        var buyer = await _dataContext.Buyers.FindAsync(code);
        
        if (buyer == null)
        {
            throw new NotFoundException(ExceptionMessageResource.BuyerNotFound);
        }
        
        _mapper.Map(updateBuyerRequest, buyer);

        _dataContext.Buyers.Update(buyer);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteBuyerAsync(int code)
    {
        var buyer = await _dataContext.Buyers.FindAsync(code);
        
        if (buyer == null)
        {
            throw new NotFoundException(ExceptionMessageResource.BuyerNotFound);
        }

        _dataContext.Buyers.Remove(buyer);
        await _dataContext.SaveChangesAsync();
    }
}