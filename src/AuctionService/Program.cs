using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using AuctionService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure the application to use PostgreSQL as the database provider for the AuctionDbContext.
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});

// Register AutoMapper to handle object mapping between DTO's and entities.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register MassTransit services to the Dependency Injection (DI) container.
builder.Services.AddMassTransit(x =>
{
    // Configure the Entity Framework outbox pattern to be used with MassTransit.
    // This ensures that messages are only published when the transaction is successful.
    x.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
    {
        // Set the delay for querying the outbox to 10 seconds.
        o.QueryDelay = TimeSpan.FromSeconds(10);
        // Configure the outbox to use PostgreSQL as the database.
        o.UsePostgres();
        // Use the bus outbox for ensuring message delivery reliability.
        o.UseBusOutbox();
    });

    // Register all consumers found in the same namespace as AuctionCreatedFaultConsumer.
    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
    // Set the endpoint name formatter to use kebab-case naming convention with a 'auction' prefix.
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));
    // Configure MassTransit to use RabbitMQ as the transport.
    x.UsingRabbitMq((context, cfg) =>
    {
        // Automatically configure RabbitMQ endpoints based on registerd consumers.
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

// Initialize database with dummy data to work with.
try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
