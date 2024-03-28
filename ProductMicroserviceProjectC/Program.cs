using Microsoft.EntityFrameworkCore;
using ProductMicroserviceProject.Models;
using ProductMicroserviceProjectC.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//by Amir
builder.Services.AddDbContext<ProductDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ProductDB")
    ));

builder.Services.AddTransient<IProductRepository, ProductRepository>();
//by Amir

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
