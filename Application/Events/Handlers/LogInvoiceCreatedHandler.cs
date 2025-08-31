using MediatR;
using Microsoft.Extensions.Logging;

namespace mediatr.Application.Events.Handlers;

public sealed class LogInvoiceCreatedHandler : INotificationHandler<InvoiceCreated>
{
    private readonly ILogger<LogInvoiceCreatedHandler> _logger;

    public LogInvoiceCreatedHandler(ILogger<LogInvoiceCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(InvoiceCreated notification, CancellationToken ct)
    {
        _logger.LogInformation("InvoiceCreated handled (logger): {notification.Number} {Amount} {Currency} for {CustomerId} at {CreatedAtUtc}",
            notification.Number, notification.Amount, notification.Currency, notification.CustomerId, notification.CreatedAtUtc);

        return Task.CompletedTask;
    }
}