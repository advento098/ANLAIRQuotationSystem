namespace ANLairQuotationSystem.Common;

public class ErrorResponse(string message)
{
    public string Message { get; private set; } = message;
}
