using System.Text.Json.Serialization;

namespace Application.Common;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    
    [JsonIgnore]
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    
    public static ApiResult<T> Successful(T result)
    {
        var response = new ApiResult<T> { Success = true, Data = result, Message = ResponseMessage.Success };

        return response;
    }

}



public enum ErrorCode
{
    None,
    EntityNotFound,
    ValidationFailed,
    UnauthorizedAccess,
    InternalError
}

public static class ResponseMessage
{
    public const string RetrievedSuccessful = "Retrieved Successfully";
    public const string Success = "Successful";
    public const string ErrorOccurred = "An Error occurred";
}
