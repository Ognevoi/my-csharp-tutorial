using MediatR;
using mediatr.Application.Events;
using mediatr.Domain;

namespace mediatr.Application.Commands;

public sealed class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IPublisher _publisher; // можно также IMediator; IPublisher — для публикации уведомлений

    public CreateInvoiceHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken ct)
    {
        // 1) Доменная логика создания счета (упрощённо — без БД)
        var invoice = new Invoice(
            customerId: request.CustomerId,
            number: request.Number,
            amount: request.Amount,
            currency: request.Currency);

        // 2) Публикуем доменное событие
        var @event = new InvoiceCreated(
            InvoiceId: invoice.Id,
            CustomerId: invoice.CustomerId,
            Number: invoice.Number,
            Amount: invoice.Amount,
            Currency: invoice.Currency,
            CreatedAtUtc: invoice.CreatedAtUtc);

        await _publisher.Publish(@event, ct);

        // 3) Возвращаем DTO
        return new InvoiceDto(
            invoice.Id,
            invoice.CustomerId,
            invoice.Number,
            invoice.Amount,
            invoice.Currency,
            invoice.CreatedAtUtc);
    }
}