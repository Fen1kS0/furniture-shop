namespace FurnitureShop.Backend.DataAccess.Entities;

public class Sale
{
    public int ContractNumber { get; set; }
    public Contract Contract { get; set; }
    public int FurnitureModel { get; set; }
    public Furniture Furniture { get; set; }
    public int Count { get; set; }
}