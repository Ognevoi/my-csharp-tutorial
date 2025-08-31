namespace mediatr.Domain;

public sealed class Invoice
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public string Number { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public DateTime CreatedAtUtc { get; }

    public Invoice(Guid customerId, string number, decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Invoice number is required.", nameof(number));
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        Id = Guid.NewGuid();
        CustomerId = customerId;
        Number = number;
        Amount = amount;
        Currency = currency.ToUpperInvariant();
        CreatedAtUtc = DateTime.UtcNow;
    }
}