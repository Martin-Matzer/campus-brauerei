
using BrauereiApi.Services;
using BrauereiApi.Options;
using BrauereiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Für Punkt 2: Web API Controller
builder.Services.AddControllers();

// Für Punkt 2: Entity Framework InMemory
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseInMemoryDatabase("InventoryDB"));

builder.Services.AddControllers();

builder.Services.Configure<InventoryBoundariesOptions>(
    builder.Configuration.GetSection(InventoryBoundariesOptions.SectionName));

builder.Services.AddScoped<IStockMovementService, StockMovementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Für Punkt 2: Controller-Routen aktivieren
app.MapControllers();

app.Run();

