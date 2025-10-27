using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration
       .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
var authSchema = "EShoppingGatewayAuthSchema";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(authSchema,options =>
    {
        options.Authority = "https://host.docker.internal:9009";
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:9009",
            ValidateAudience = true,
            ValidAudience = "EShoppingGateway",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero

        };
        //Add this to docker to host communtication
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"======= AUTHENTICTION FAILED");
                Console.WriteLine($"Exception :{context.Exception.Message}");
                Console.WriteLine($"Authority:{options.Authority}");
                return Task.CompletedTask;
            }
        };

    });


builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello ocelot"); });
});

await app.UseOcelot();

await app.RunAsync();
