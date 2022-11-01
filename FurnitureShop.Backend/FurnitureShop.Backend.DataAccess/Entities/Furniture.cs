namespace FurnitureShop.Backend.DataAccess.Entities;

public class Furniture
{
    public int Model { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Characteristics { get; set; }

    public ICollection<Sale> Sales { get; set; }
}