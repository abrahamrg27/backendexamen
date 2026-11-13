using backend.DTOs;
using backend.Models;
using backend.Repository;
using backend.services;
using backend.Validators;
using Backend.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

// Validators Beer
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

// Validators Brand
builder.Services.AddScoped<IValidator<BrandInsertDto>, BrandInsertValidator>();
builder.Services.AddScoped<IValidator<BrandUpdateDto>, BrandUpdateValidator>();

// Validators Inventory
builder.Services.AddScoped<IValidator<InventoryInsertDto>, InventoryInsertValidator>();
builder.Services.AddScoped<IValidator<InventoryUpdateDto>, InventoryUpdateValidator>();

// Services
builder.Services.AddKeyedScoped<ICommonServices<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");
builder.Services.AddKeyedScoped<ICommonServices<BrandDto, BrandInsertDto, BrandUpdateDto>, BrandService>("brandService");
builder.Services.AddKeyedScoped<ICommonServices<InventoryDto, InventoryInsertDto, InventoryUpdateDto>, InventoryService>("inventoryService");

// Repositories
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Inventory>, InventoryRepository>();

// Post service
builder.Services.AddHttpClient<IPostService, PostServices>(c => c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]));

// Database
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
