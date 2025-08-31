namespace mediatr.Infrastructure.Email;

public sealed class ConsoleEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        Console.WriteLine("=== Simulated E-mail ===");
        Console.WriteLine($"TO: {to}");
        Console.WriteLine($"SUBJECT: {subject}");
        Console.WriteLine(body);
        Console.WriteLine("========================");
        return Task.CompletedTask;
    }
}