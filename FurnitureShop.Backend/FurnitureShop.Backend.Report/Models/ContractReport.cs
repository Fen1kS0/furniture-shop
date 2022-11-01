namespace FurnitureShop.Backend.Report.Models;

public record ContractReport
{
    public int ContractNumber { get; set; }
    public ICollection<SaleEntity> Sales { get; set; } = new List<SaleEntity>();
    public int TotalPrice => Sales.Sum(s => s.TotalPrice);
}