
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace reservation_system.Exceptions;

internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync
     (HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
            logger.LogError(ex, "Unhandled exception occured");

            context.Response.StatusCode = ex switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new ProblemDetails
                {
                    Type = ex.GetType().Name,
                    Title = "An error occured",
                    Detail = ex.Message
                }
            
            });       
    }
}