using Microsoft.EntityFrameworkCore;
using Customer.Application.Mappings;
using Customer.Application.Services;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Customer.Infrastructure.Repositories;
using Customer.API.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .MinimumLevel.Information() 
        .WriteTo.Console()         
        .WriteTo.File("Logs/customer-log-.txt", rollingInterval: RollingInterval.Day); 
});

// Agregar servicios de Controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CustomerMappingProfile>();
});

// Inyección de Dependencias
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agregar el middleware de manejo global de excepciones
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();