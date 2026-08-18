namespace ANLairQuotationSystem.Common;

public class Result<T>
{
    public T? Value { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }

    public static Result<T> Ok(T value, string message = "Success")
    {
        return new Result<T> { Value = value, Message = message, IsSuccess = true };
    }

    public static Result<T> Fail(string message = "Failed")
    {
        return new Result<T> { Message = message, IsSuccess = false };
    }
}

