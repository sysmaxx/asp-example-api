using Microsoft.EntityFrameworkCore;
using Service.Abstractions.Services;
using Service.Api.Infrasturcture;
using Service.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseInMemoryDatabase("MyInMemoryDb"));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
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

// Seed in-memory-db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ProductDbContext>();
    dbContext.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
