namespace FurnitureShop.Backend.DataAccess.Entities;

public class Buyer
{
    public int Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string NumberPhone { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}