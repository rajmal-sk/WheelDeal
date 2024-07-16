using MassTransit;
using NotificationService.Consumers;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Register MassTransit services to the Dependency Injection (DI) container.
builder.Services.AddMassTransit(x =>
{
    // Register all consumers found in the same namespace as AuctionCreatedFaultConsumer.
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    // Set the endpoint name formatter to use kebab-case naming convention with a 'auction' prefix.
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("notification", false));
    // Configure MassTransit to use RabbitMQ as the transport.
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        // Automatically configure RabbitMQ endpoints based on registerd consumers.
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSignalR();
var app = builder.Build();

app.MapHub<NotificationHub>("/notifications");

app.Run();
