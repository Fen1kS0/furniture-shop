namespace FurnitureShop.Backend.Common.DTOs.Contracts;

public class ContractAddedResponse
{
    public int Number { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int BuyerCode { get; set; }
}