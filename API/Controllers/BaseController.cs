using System.Net;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    
    protected ActionResult<ApiResult<T>> HandleOperationResult<T>(ApiResult<T> response)
    {
        if (response.Success)
        {
            return Ok(response);
        }

        var statusCode = ErrorCodeToHttpStatusMapper.MapErrorCodeToHttpStatus(response.ErrorCode);
        return StatusCode((int)statusCode, response);
    }
    
    // protected ActionResult HandleOperationResult<T>(ApiResult<T> response)
    // {
    //     if (response.Success)
    //     {
    //         return Ok(response);
    //     }
    //
    //     return StatusCode((int)response.ErrorCode, response);
    // }
}

public static class ErrorCodeToHttpStatusMapper
{
    private static readonly Dictionary<ErrorCode, HttpStatusCode> ErrorCodeMappings = new Dictionary<ErrorCode, HttpStatusCode>
    {
        { ErrorCode.EntityNotFound, HttpStatusCode.NotFound },
        { ErrorCode.ValidationFailed, HttpStatusCode.BadRequest },
        { ErrorCode.UnauthorizedAccess, HttpStatusCode.Unauthorized },
        { ErrorCode.InternalError, HttpStatusCode.InternalServerError }
    };

    public static HttpStatusCode MapErrorCodeToHttpStatus(ErrorCode errorCode)
    {
        return ErrorCodeMappings.GetValueOrDefault(errorCode, HttpStatusCode.InternalServerError); // Default to 500 if code is unknown
    }
}


