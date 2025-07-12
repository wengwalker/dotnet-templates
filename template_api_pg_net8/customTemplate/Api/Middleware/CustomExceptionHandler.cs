﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api.Middleware;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<CustomExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

        httpContext.Response.StatusCode = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = exception is ValidationException
                ? GetValidationProblemDetails(httpContext, exception as ValidationException)
                : GetProblemDetails(httpContext, exception)
        });
    }

    private ProblemDetails GetProblemDetails(HttpContext httpContext, Exception exception)
    {
        return new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "An unexpeced error occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message
        };
    }

    private ValidationProblemDetails GetValidationProblemDetails(HttpContext httpContext, ValidationException exception)
    {
        return new ValidationProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "One or more valdiation errors occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message,
            Errors = exception.Errors.ToDictionary(g => g.PropertyName, e => new string[] { e.ErrorMessage })
        };
    }
}
