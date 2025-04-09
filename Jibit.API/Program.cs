using FluentValidation;
using FluentValidation.AspNetCore;
using Jibit.API.Validators;
using Jibit.Application.Interfaces;
using Jibit.Application.Services;
using Jibit.Application.Validators;
using Jibit.Domain;
using Jibit.Infra.Data;
using Jibit.Infra.Repositories;
using Jibit.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RequestRepository>();
builder.Services.AddHttpClient<JibitApiService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IIbanService, IbanService>();
builder.Services.AddScoped<ICardMatchingService, CardMatchingService>();
builder.Services.AddScoped<IMobileMatchingService, MobileMatchingService>();
builder.Services.AddValidatorsFromAssemblyContaining<IbanValidator>();
builder.Services.AddHttpClient<ExternalApiService>();
builder.Services.AddHttpClient<TokenService>();
builder.Services.Configure<JibitSettings>(builder.Configuration.GetSection("JibitSettings"));

// Add Swagger services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Jibit API",
        Version = "v1",
        Description = "API Documentation for Jibit Project",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@jibit.com"
        }
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jibit API v1");
        c.RoutePrefix = string.Empty; // Sets Swagger UI to the root (http://localhost:5277 or specified port).
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();