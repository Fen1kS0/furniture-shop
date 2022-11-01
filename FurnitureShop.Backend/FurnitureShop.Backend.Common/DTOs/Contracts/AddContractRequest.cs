namespace FurnitureShop.Backend.Common.DTOs.Contracts;

public class AddContractRequest
{
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int BuyerCode { get; set; }
}