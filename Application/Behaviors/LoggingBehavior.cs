using MediatR;
using Microsoft.Extensions.Logging;

namespace mediatr.Application.Behaviors;

/// <summary>
/// Простое pipeline-поведение: логирует вход и выход любого IRequest.
/// </summary>
public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        _logger.LogInformation("➡️ Handling {RequestType}: {@Request}", typeof(TRequest).Name, request);
        var response = await next();
        _logger.LogInformation("⬅️ Handled {RequestType} -> {ResponseType}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}