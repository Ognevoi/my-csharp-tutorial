using MediatR;
using mediatr.Application.Behaviors;
using mediatr.Application.Commands;
using mediatr.Infrastructure.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

/*
MediatR - implements the Mediator pattern, helping reduce coupling between components.

Core idea: Instead of calling methods directly, classes send requests or notifications to IMediator, which routes them to the right handler.
Uses: Common in CQRS (separating commands and queries).
*/

// Логи
services.AddLogging(b => b.AddSimpleConsole(o =>
{
    o.SingleLine = true;
    o.TimestampFormat = "HH:mm:ss ";
}).SetMinimumLevel(LogLevel.Information));

// MediatR — регистрируем все обработчики из текущей сборки
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Pipeline-поведение
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Инфраструктура (e-mail)
services.AddSingleton<IEmailSender, ConsoleEmailSender>();

var provider = services.BuildServiceProvider();

var logger = provider.GetRequiredService<ILogger<Program>>();
logger.LogInformation("App started");

// Имитация входных данных
var command = new CreateInvoiceCommand(
    CustomerId: Guid.NewGuid(),
    Number: "INV-2025-0001",
    Amount: 199.99m,
    Currency: "EUR");

// Отправляем команду
var mediator = provider.GetRequiredService<IMediator>();
var result = await mediator.Send(command);

logger.LogInformation("Created invoice DTO: {Invoice}", $"{result.Number} | {result.Amount} {result.Currency} | {result.CustomerId}");

logger.LogInformation("Done.");