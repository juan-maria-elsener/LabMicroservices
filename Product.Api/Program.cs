using Microsoft.EntityFrameworkCore;
using Product.Application.Mappings;
using Product.Application.Services;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProductMappingProfile>();
});

// Inyección de Dependencias (DI)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
