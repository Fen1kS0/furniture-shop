using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureShop.Backend.DataAccess;
using FurnitureShop.Backend.Common.DTOs.Contracts;
using FurnitureShop.Backend.BL.Interfaces.Services;
using FurnitureShop.Backend.Common.Exceptions;
using FurnitureShop.Backend.Common.Resources;
using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Backend.BL.Services;

public class ContractService : IContractService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ContractService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContractResponse>> GetAllContractsAsync()
    {
        var contracts = await _dataContext.Contracts
            .AsSingleQuery()
            .AsNoTracking()
            .ProjectTo<ContractResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return contracts;
    }

    public async Task<ContractResponse> GetContractByNumberAsync(int number)
    {
        var contract = await _dataContext.Contracts
            .AsSingleQuery()
            .AsNoTracking()
            .Where(c => c.Number == number)
            .ProjectTo<ContractResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (contract == null)
        {
            throw new NotFoundException(ExceptionMessageResource.ContractNotFound);
        }

        return contract;
    }

    public async Task<ContractAddedResponse> AddContractAsync(AddContractRequest addContractRequest)
    {
        await BuyerExistsAsync(addContractRequest.BuyerCode);
        
        var contract = _mapper.Map<Contract>(addContractRequest);

        var result = await _dataContext.Contracts.AddAsync(contract);
        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<ContractAddedResponse>(result.Entity);
    }

    public async Task UpdateContractAsync(int number, UpdateContractRequest updateContractRequest)
    {
        var contract = await _dataContext.Contracts.FindAsync(number);

        if (contract == null)
        {
            throw new NotFoundException(ExceptionMessageResource.ContractNotFound);
        }

        await BuyerExistsAsync(updateContractRequest.BuyerCode);

        _mapper.Map(updateContractRequest, contract);

        _dataContext.Contracts.Update(contract);
        await _dataContext.SaveChangesAsync();
    }
    
    public async Task DeleteContractAsync(int number)
    {
        var contract = await _dataContext.Contracts.FindAsync(number);
        
        if (contract == null)
        {
            throw new NotFoundException(ExceptionMessageResource.ContractNotFound);
        }

        _dataContext.Contracts.Remove(contract);
        await _dataContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Checking for the existence of a buyer
    /// </summary>
    /// <exception cref="NotFoundException">If buyer not found</exception>
    private async Task BuyerExistsAsync(int buyerCode)
    {
        var buyer = await _dataContext.Buyers.FindAsync(buyerCode);
        
        if (buyer == null)
        {
            throw new NotFoundException(ExceptionMessageResource.BuyerNotFound);
        }
    }
}