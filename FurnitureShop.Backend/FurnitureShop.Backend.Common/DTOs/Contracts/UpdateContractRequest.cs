namespace FurnitureShop.Backend.Common.DTOs.Contracts;

public class UpdateContractRequest
{
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int BuyerCode { get; set; }
}