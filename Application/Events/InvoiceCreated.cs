using MediatR;

namespace mediatr.Application.Events;

/// <summary>
/// Доменное событие/уведомление: счёт создан.
/// Позволяет реактивно запустить побочные действия (лог, e-mail, интеграции).
/// </summary>
public sealed record InvoiceCreated(
    Guid InvoiceId,
    Guid CustomerId,
    string Number,
    decimal Amount,
    string Currency,
    DateTime CreatedAtUtc
) : INotification;