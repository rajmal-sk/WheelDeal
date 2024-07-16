using BiddingService.Consumers;
using BiddingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Register MassTransit services to the Dependency Injection (DI) container.
builder.Services.AddMassTransit(x =>
{
    // Register all consumers found in the same namespace as AuctionCreatedFaultConsumer.
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    // Set the endpoint name formatter to use kebab-case naming convention with a 'auction' prefix.
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));
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

// Add authentication services using JWT Bearer tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Set the authority to validate the tokens
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        // Disable HTTPS metadata requirement (useful for development environments)
        options.RequireHttpsMetadata = false;
        // Configure token validation parameters
        options.TokenValidationParameters.ValidateAudience = false; // Do not validate audience
        options.TokenValidationParameters.NameClaimType = "username"; // Set the name claim type to "username"
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<CheckAuctionFinished>();
builder.Services.AddScoped<GrpcAuctionClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();

await DB.InitAsync("BidDb",
    MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("BidDbConnection")));
app.Run();

