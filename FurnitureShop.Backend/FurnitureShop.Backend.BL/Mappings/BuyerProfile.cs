using AutoMapper;
using FurnitureShop.Backend.DataAccess.Entities;
using FurnitureShop.Backend.Common.DTOs.Buyers;

namespace FurnitureShop.Backend.BL.Mappings;

public class BuyerProfile : Profile
{
    public BuyerProfile()
    {
        CreateMap<AddBuyerRequest, Buyer>()
            .ForMember(b => b.Name, 
                m => m.MapFrom(b => b.Name))
            .ForMember(b => b.Address, 
                m => m.MapFrom(b => b.Address))
            .ForMember(b => b.NumberPhone, 
                m => m.MapFrom(b => b.NumberPhone));
        
        CreateMap<UpdateBuyerRequest, Buyer>()
            .ForMember(b => b.Name, 
                m => m.MapFrom(b => b.Name))
            .ForMember(b => b.Address, 
                m => m.MapFrom(b => b.Address))
            .ForMember(b => b.NumberPhone, 
                m => m.MapFrom(b => b.NumberPhone));
        
        CreateMap<Buyer, BuyerResponse>()
            .ForMember(b => b.Code, 
                m => m.MapFrom(b => b.Code))
            .ForMember(b => b.Name, 
                m => m.MapFrom(b => b.Name))
            .ForMember(b => b.Address, 
                m => m.MapFrom(b => b.Address))
            .ForMember(b => b.NumberPhone, 
                m => m.MapFrom(b => b.NumberPhone));
        
        CreateMap<Buyer, BuyerAddedResponse>()
            .ForMember(b => b.Code, 
                m => m.MapFrom(b => b.Code))
            .ForMember(b => b.Name, 
                m => m.MapFrom(b => b.Name))
            .ForMember(b => b.Address, 
                m => m.MapFrom(b => b.Address))
            .ForMember(b => b.NumberPhone, 
                m => m.MapFrom(b => b.NumberPhone));
    }
}