using MassTransit;
using SearchService.Consumers;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register MassTransit services to the Dependency Injection (DI) container.
builder.Services.AddMassTransit(x =>
{
    // Register all consumers found in the same namespace as AuctionCreatedConsumer.
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    // Set the endpoint name formatter to use kebab-case naming convention with a 'search' prefix.
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    // Configure MassTransit to use RabbitMQ as the transport.
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        // Configures a receive endpoint for RabbitMQ
        cfg.ReceiveEndpoint("search-auction-created", e =>
        {
            // Sets up message retry policy to retry 5 times with 5 seconds interval between each retry.
            e.UseMessageRetry(r => r.Interval(5, 5));
            // Configures the consumer for the AuctionCreated messages.
            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
        // Automatically configure RabbitMQ endpoints based on registerd consumers.
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

try
{
    await DbInitializer.InitDb(app); // Initialize the database asynchronously.
}
catch (Exception e)
{

    Console.WriteLine(e); // Handle and log any exceptions during database initialization.
}

app.Run();
