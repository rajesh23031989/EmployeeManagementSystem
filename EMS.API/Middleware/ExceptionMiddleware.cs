using EMS.Application.Common;
using EMS.Application.Exceptions;
using System.Text.Json;

namespace EMS.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var response = new ApiResponse<object>();

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.StatusCode = 404;
                response.Message = exception.Message;
                break;

            case BadRequestException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = 400;
                response.Message = exception.Message;
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = 500;
                response.Message = "An unexpected error occurred.";
                break;
        }

        response.Success = false;

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}