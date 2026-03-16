using Estacionamento.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado.");
            await HandleAsync(context, exception);
        }
    }

    private static async Task HandleAsync(HttpContext context, Exception exception)
    {
        ProblemDetails problemDetails;

        context.Response.ContentType = "application/problem+json";

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro de validação.",
                Detail = "Um ou mais erros de validação ocorreram.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7807"
            };

            problemDetails.Extensions["errors"] = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());
        }
        else if (exception is AppException appException)
        {
            context.Response.StatusCode = appException.StatusCode;
            problemDetails = new ProblemDetails
            {
                Status = appException.StatusCode,
                Title = "Erro na requisição.",
                Detail = appException.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7807"
            };
        }
        else if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Não autorizado.",
                Detail = exception.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7807"
            };
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno do servidor.",
                Detail = "Ocorreu um erro interno não esperado.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7807"
            };
        }
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

}
