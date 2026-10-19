using BrauereiApi.Services;
using BrauereiApi.Options;
using BrauereiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Für Punkt 2: Web API Controller
builder.Services.AddControllers();

builder.Services.AddOpenApi();  

// Für Punkt 2: Entity Framework InMemory
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseInMemoryDatabase("InventoryDB"));

builder.Services.Configure<InventoryBoundariesOptions>(
    builder.Configuration.GetSection(InventoryBoundariesOptions.SectionName));

builder.Services.AddScoped<IStockMovementService, StockMovementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "BrauereiApi v1");
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();

// Für Punkt 2: Controller-Routen aktivieren
app.MapControllers();

app.Run();
