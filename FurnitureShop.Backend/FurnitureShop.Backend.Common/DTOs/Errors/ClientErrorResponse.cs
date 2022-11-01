namespace FurnitureShop.Backend.Common.DTOs.Errors;

public class ClientErrorResponse
{
    public ClientErrorResponse(int statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
}
