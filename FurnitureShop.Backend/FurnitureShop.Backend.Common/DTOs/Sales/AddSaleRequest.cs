namespace FurnitureShop.Backend.Common.DTOs.Sales;

public class AddSaleRequest
{
    public int ContractNumber { get; set; }
    public int FurnitureModel { get; set; }
    public int Count { get; set; }
}