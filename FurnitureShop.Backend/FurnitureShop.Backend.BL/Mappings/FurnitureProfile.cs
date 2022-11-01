using AutoMapper;
using FurnitureShop.Backend.DataAccess.Entities;
using FurnitureShop.Backend.Common.DTOs.Furniture;

namespace FurnitureShop.Backend.BL.Mappings;

public class FurnitureProfile : Profile
{
    public FurnitureProfile()
    {
        CreateMap<AddFurnitureRequest, Furniture>()
            .ForMember(f => f.Name, 
                m => m.MapFrom(f => f.Name))
            .ForMember(f => f.Price, 
                m => m.MapFrom(f => f.Price))
            .ForMember(f => f.Characteristics, 
                m => m.MapFrom(f => f.Characteristics));
        
        CreateMap<UpdateFurnitureRequest, Furniture>()
            .ForMember(f => f.Name, 
                m => m.MapFrom(f => f.Name))
            .ForMember(f => f.Price, 
                m => m.MapFrom(f => f.Price))
            .ForMember(f => f.Characteristics, 
                m => m.MapFrom(f => f.Characteristics));
        
        CreateMap<Furniture, FurnitureResponse>()
            .ForMember(f => f.Model, 
                m => m.MapFrom(f => f.Model))
            .ForMember(f => f.Name, 
                m => m.MapFrom(f => f.Name))
            .ForMember(f => f.Price, 
                m => m.MapFrom(f => f.Price))
            .ForMember(f => f.Characteristics, 
                m => m.MapFrom(f => f.Characteristics));
        
        CreateMap<Furniture, FurnitureAddedResponse>()
            .ForMember(f => f.Model, 
                m => m.MapFrom(f => f.Model))
            .ForMember(f => f.Name, 
                m => m.MapFrom(f => f.Name))
            .ForMember(f => f.Price, 
                m => m.MapFrom(f => f.Price))
            .ForMember(f => f.Characteristics, 
                m => m.MapFrom(f => f.Characteristics));
    }
}