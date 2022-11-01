namespace FurnitureShop.Backend.DataAccess.Entities;

public class Contract
{
    public int Number { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int BuyerCode { get; set; }
    public Buyer Buyer { get; set; }

    public ICollection<Sale> Sales { get; set; }
}