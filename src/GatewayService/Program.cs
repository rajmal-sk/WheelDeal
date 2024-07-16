using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add reverse proxy services and load configuration from the app settings
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("customPolicy", b =>
    {
        b.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(builder.Configuration["ClientApp"]);
    });
});

var app = builder.Build();

app.UseCors();

// Map the reverse proxy routes
app.MapReverseProxy();

// Use the authentication middleware
app.UseAuthentication();
// Use the authorization middleware
app.UseAuthorization();

app.Run();
