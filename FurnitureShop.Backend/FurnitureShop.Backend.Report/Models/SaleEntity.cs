namespace FurnitureShop.Backend.Report.Models;

public record SaleEntity
{
    public string FurnitureName { get; set; }
    public int FurnitureModel { get; set; }
    public int Count { get; set; }
    public int FurniturePrice { get; set; }
    public int TotalPrice => FurniturePrice * Count;
}