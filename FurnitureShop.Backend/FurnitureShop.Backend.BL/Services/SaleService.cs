using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Common.DTOs.Sales;
using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.Exceptions;
using FurnitureShop.Backend.Common.Resources;
using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Backend.BL.Services;

public class SaleService : ISaleService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public SaleService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleResponse>> GetAllSaleAsync()
    {
        var sales = await _dataContext.Sales
            .AsSingleQuery()
            .AsNoTracking()
            .ProjectTo<SaleResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return sales;
    }

    public async Task<IEnumerable<SaleResponse>> GetSaleByContractAsync(int contractNumber)
    {
        var sales = await _dataContext.Sales
            .AsSingleQuery()
            .AsNoTracking()
            .Where(s => s.ContractNumber == contractNumber)
            .ProjectTo<SaleResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return sales;
    }

    public async Task<IEnumerable<SaleResponse>> GetSaleByFurnitureAsync(int furnitureModel)
    {
        var sales = await _dataContext.Sales
            .AsSingleQuery()
            .AsNoTracking()
            .Where(s => s.FurnitureModel == furnitureModel)
            .ProjectTo<SaleResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return sales;
    }

    public async Task<SaleAddedResponse> AddSaleAsync(AddSaleRequest addSaleRequest)
    {
        await ContractExistsAsync(addSaleRequest.ContractNumber);
        await FurnitureExistsAsync(addSaleRequest.FurnitureModel);
        
        var sale = _mapper.Map<Sale>(addSaleRequest);

        var result = await _dataContext.Sales.AddAsync(sale);
        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<SaleAddedResponse>(result.Entity);
    }

    public async Task UpdateSaleAsync(int contractNumber, int furnitureModel, UpdateSaleRequest updateSaleRequest)
    {
        var sale = await _dataContext.Sales.FindAsync(contractNumber, furnitureModel);
        
        if (sale == null)
        {
            throw new NotFoundException(ExceptionMessageResource.SaleNotFound);
        }
        
        _mapper.Map(updateSaleRequest, sale);

        _dataContext.Sales.Update(sale);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteSaleAsync(int contractNumber, int furnitureModel)
    {
        var sale = await _dataContext.Sales.FindAsync(contractNumber, furnitureModel);
        
        if (sale == null)
        {
            throw new NotFoundException(ExceptionMessageResource.SaleNotFound);
        }

        _dataContext.Sales.Remove(sale);
        await _dataContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Checking for the existence of a contract
    /// </summary>
    /// <exception cref="NotFoundException">If contract not found</exception>
    private async Task ContractExistsAsync(int contractNumber)
    {
        var contract = await _dataContext.Contracts.FindAsync(contractNumber);
        
        if (contract == null)
        {
            throw new NotFoundException(ExceptionMessageResource.ContractNotFound);
        }
    }
    
    /// <summary>
    /// Checking for the existence of a furniture
    /// </summary>
    /// <exception cref="NotFoundException">If furniture not found</exception>
    private async Task FurnitureExistsAsync(int furnitureModel)
    {
        var furniture = await _dataContext.Furniture.FindAsync(furnitureModel);
        
        if (furniture == null)
        {
            throw new NotFoundException(ExceptionMessageResource.FurnitureNotFound);
        }
    }
}