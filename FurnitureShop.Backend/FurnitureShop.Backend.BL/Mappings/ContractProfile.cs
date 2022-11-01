using AutoMapper;
using FurnitureShop.Backend.DataAccess.Entities;
using FurnitureShop.Backend.Common.DTOs.Contracts;

namespace FurnitureShop.Backend.BL.Mappings;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<AddContractRequest, Contract>()
            .ForMember(c => c.BuyerCode, 
                m => m.MapFrom(c => c.BuyerCode))
            .ForMember(c => c.IssueDate, 
                m => m.MapFrom(c => c.IssueDate.ToUniversalTime()))
            .ForMember(c => c.DueDate, 
                m => m.MapFrom(c => c.DueDate.ToUniversalTime()));
        
        CreateMap<UpdateContractRequest, Contract>()
            .ForMember(c => c.BuyerCode, 
                m => m.MapFrom(c => c.BuyerCode))
            .ForMember(c => c.IssueDate, 
                m => m.MapFrom(c => c.IssueDate.ToUniversalTime()))
            .ForMember(c => c.DueDate, 
                m => m.MapFrom(c => c.DueDate.ToUniversalTime()));
        
        CreateMap<Contract, ContractResponse>()
            .ForMember(c => c.Number, 
                m => m.MapFrom(c => c.Number))
            .ForMember(c => c.BuyerCode, 
                m => m.MapFrom(c => c.BuyerCode))
            .ForMember(c => c.BuyerName, 
                m => m.MapFrom(c => c.Buyer.Name))
            .ForMember(c => c.IssueDate, 
                m => m.MapFrom(c => c.IssueDate.ToLocalTime()))
            .ForMember(c => c.DueDate, 
                m => m.MapFrom(c => c.DueDate.ToLocalTime()));
        
        CreateMap<Contract, ContractAddedResponse>()
            .ForMember(c => c.Number, 
                m => m.MapFrom(c => c.Number))
            .ForMember(c => c.BuyerCode, 
                m => m.MapFrom(c => c.BuyerCode))
            .ForMember(c => c.IssueDate, 
                m => m.MapFrom(c => c.IssueDate.ToLocalTime()))
            .ForMember(c => c.DueDate, 
                m => m.MapFrom(c => c.DueDate.ToLocalTime()));
    }
}