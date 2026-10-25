using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Exceptions;

namespace TaskManagerApi.Common;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (httpContext.Response.HasStarted)
        {
            _logger.LogWarning(exception,"Response has already started");
            return false;
        }

        if (exception is OperationCanceledException or TaskCanceledException)
        {
            _logger.LogWarning(exception,"Operation canceled by client");
            return true;
        }
        
        var (status, title, detail) = exception switch
        {
            CustomException ex => ((int)ex.StatusCode, ex.Title, ex.Detail),
            DbUpdateException => (409, "Resource Already Exists", "The operation could not be completed because the resource or a unique field already exists"),
            _ => (500, "Internal Server Error", "Something unexpected happened, please try again later")
        };

        if (exception is CustomException customException)
        {
            _logger.LogWarning("Business Exception captured: {Message}", customException.Message);
        }
        else
        {
            _logger.LogError(exception, "Unexpected error");
        }

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };
        
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true; 
    }
}