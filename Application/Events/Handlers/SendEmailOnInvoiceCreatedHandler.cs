using MediatR;
using mediatr.Infrastructure.Email;
using Microsoft.Extensions.Logging;

namespace mediatr.Application.Events.Handlers;

public sealed class SendEmailOnInvoiceCreatedHandler : INotificationHandler<InvoiceCreated>
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<SendEmailOnInvoiceCreatedHandler> _logger;

    public SendEmailOnInvoiceCreatedHandler(IEmailSender emailSender, ILogger<SendEmailOnInvoiceCreatedHandler> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Handle(InvoiceCreated notification, CancellationToken ct)
    {
        // В реальной жизни – подтянем e-mail клиента из БД.
        var to = "customer@example.com";
        var subject = $"Your invoice {notification.Number}";
        var body = $"Dear customer,\n\nYour invoice {notification.Number} for {notification.Amount} {notification.Currency} has been created.\n\nThanks!";

        await _emailSender.SendAsync(to, subject, body, ct);

        _logger.LogInformation("InvoiceCreated handled (email): sent to {To} for invoice {Number}", to, notification.Number);
    }
}