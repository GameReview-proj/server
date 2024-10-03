using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameReview.Services.Exceptions;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            ConflictException => StatusCodes.Status409Conflict,
            ExternalAPIException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        Console.WriteLine($"Exception {DateTime.Now} ({context.Exception.Message}) - {context.Exception.StackTrace}");

        context.Result = new ObjectResult(new
        {
            error = context.Exception.Message
        })
        {
            StatusCode = statusCode
        };
    }
}