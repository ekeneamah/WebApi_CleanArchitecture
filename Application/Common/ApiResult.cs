using System.Text.Json.Serialization;

namespace Application.Common;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
    
    [JsonIgnore]
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    
    public static ApiResult<T> Successful(T? result, string? message = null)
    {
        var response = new ApiResult<T> { Success = true, Data = result, Message = message ?? ResponseMessage.Success };

        return response;
    }
    public static ApiResult<T> NotFound(string? message = null)
    {
        var response = new ApiResult<T> { Success = false, Message  = message ?? ResponseMessage.NotFound, ErrorCode = ErrorCode.EntityNotFound };

        return response;
    }

    public static ApiResult<T> Failed(string? message = null)
 {
        var response = new ApiResult<T> { Success = false, Message  = message ?? ResponseMessage.BadRequest, ErrorCode = ErrorCode.ValidationFailed };

        return response;
    }
    public static ApiResult<T> FailureResult(string errorMessage)
    {
        return new ApiResult<T> { Success = false, ErrorMessage = errorMessage };
    }

}



public enum ErrorCode
{
    None,
    EntityNotFound,
    ValidationFailed,
    UnauthorizedAccess,
    InternalError,
}

public static class ResponseMessage
{
    public const string RetrievedSuccessful = "Retrieved Successfully";
    public const string Success = "Successful";
    public const string ErrorOccurred = "An Error occurred";
    public const string NotFound = "Item with specified key not found";
    public const string BadRequest = "Bad Request";
}
