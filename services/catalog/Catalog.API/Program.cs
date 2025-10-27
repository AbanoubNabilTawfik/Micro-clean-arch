using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://host.docker.internal:9009";
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:9009",
            ValidateAudience = true,
            ValidAudience="Catalog",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew=TimeSpan.Zero

        };
        //Add this to docker to host communtication
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =(message,cert,chain,errors)=>true
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanRead", policy => policy.RequireClaim("scope", "catalogapi.read"));
});

builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(GetProductByIdQuery))));


builder.Services.AddScoped<ICatalogContext,CatalogContext>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();

    var userPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();

    builder.Services.AddControllers(config =>
    {
        config.Filters.Add(new AuthorizeFilter(userPolicy));
    });

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "This is API for Catalog microservice in ecommerce application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Abanoub Nabil",
            Email = "abanoub.nabil2016@gmail.com",
            Url = new Uri("https://yourwebsite.eg")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: **Bearer {your JWT}**",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
