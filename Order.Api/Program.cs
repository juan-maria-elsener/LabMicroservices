using Microsoft.EntityFrameworkCore;
using Order.Application.Integrations;
using Order.Application.Mappings;
using Order.Application.Service;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.HttpClients;
using Order.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

// Automapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<OrderMappingProfile>();
});

// Inyección de Dependencias 
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddHttpClient<ICustomerIntegration, CustomerHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:CustomerApi"]!);
});

builder.Services.AddHttpClient<IProductIntegration, ProductHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:ProductApi"]!);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();