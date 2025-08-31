using MediatR;

namespace mediatr.Application.Commands;

public sealed record CreateInvoiceCommand(
    Guid CustomerId,
    string Number,
    decimal Amount,
    string Currency
) : IRequest<InvoiceDto>;

public sealed record InvoiceDto(
    Guid Id,
    Guid CustomerId,
    string Number,
    decimal Amount,
    string Currency,
    DateTime CreatedAtUtc
);