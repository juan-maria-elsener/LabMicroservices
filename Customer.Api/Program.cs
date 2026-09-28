using Microsoft.EntityFrameworkCore;
using Customer.Application.Mappings;
using Customer.Application.Services;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Customer.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();