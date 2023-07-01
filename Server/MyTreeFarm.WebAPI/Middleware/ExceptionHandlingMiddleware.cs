using System;
using System.Text.Json;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using ValidationException = AP.MyTreeFarm.Application.Exceptions.ValidationException;

namespace AP.MyTreeFarm.WebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = new ErrorResponseInfo();
            response.Message = ex.Message;
            switch (ex)
            {
                case ValidationException:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case RelationNotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

public class ErrorResponseInfo
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}
