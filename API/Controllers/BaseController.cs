using System.Net;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleOperationResult<T>(ApiResult<T> result)
    {
        if (result.Success)
        {
            return Ok(result.Data);
        }

        var statusCode = ErrorCodeToHttpStatusMapper.MapErrorCodeToHttpStatus(result.ErrorCode);
        return StatusCode((int)statusCode, result);
    }
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


