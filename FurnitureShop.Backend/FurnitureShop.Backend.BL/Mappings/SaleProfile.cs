using AutoMapper;
using FurnitureShop.Backend.Common.DTOs.Sales;
using FurnitureShop.Backend.DataAccess.Entities;

namespace FurnitureShop.Backend.BL.Mappings;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<AddSaleRequest, Sale>()
            .ForMember(s => s.ContractNumber,
                m => m.MapFrom(s => s.ContractNumber))
            .ForMember(s => s.FurnitureModel,
                m => m.MapFrom(s => s.FurnitureModel))
            .ForMember(s => s.Count,
                m => m.MapFrom(s => s.Count));
        
        CreateMap<UpdateSaleRequest, Sale>()
            .ForMember(s => s.Count,
                m => m.MapFrom(s => s.Count));
        
        CreateMap<Sale, SaleResponse>()
            .ForMember(s => s.ContractNumber,
                m => m.MapFrom(s => s.ContractNumber))
            .ForMember(s => s.FurnitureModel,
                m => m.MapFrom(s => s.FurnitureModel))
            .ForMember(s => s.FurnitureName,
                m => m.MapFrom(s => s.Furniture.Name))
            .ForMember(s => s.Count,
                m => m.MapFrom(s => s.Count));
        
        CreateMap<Sale, SaleAddedResponse>()
            .ForMember(s => s.ContractNumber,
                m => m.MapFrom(s => s.ContractNumber))
            .ForMember(s => s.FurnitureModel,
                m => m.MapFrom(s => s.FurnitureModel))
            .ForMember(s => s.Count,
                m => m.MapFrom(s => s.Count));
    }
}