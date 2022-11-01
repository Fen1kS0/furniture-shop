using AutoMapper;
using FurnitureShop.Backend.DataAccess.Entities;
using FurnitureShop.Backend.Report.Models;

namespace FurnitureShop.Backend.Report.Mappings;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Sale, SaleEntity>()
            .ForMember(s => s.Count,
                m => m.MapFrom(s => s.Count))
            .ForMember(s => s.FurnitureModel,
                m => m.MapFrom(s => s.FurnitureModel))
            .ForMember(s => s.FurnitureName,
                m => m.MapFrom(s => s.Furniture.Name))
            .ForMember(s => s.FurniturePrice,
                m => m.MapFrom(s => s.Furniture.Price));

        CreateMap<Contract, ContractReport>()
            .ForMember(c => c.ContractNumber,
                m => m.MapFrom(c => c.Number))
            .ForMember(c => c.Sales,
                m => m.MapFrom(c => c.Sales));
    }
}