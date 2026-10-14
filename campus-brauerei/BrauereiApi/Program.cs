
using BrauereiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Für Punkt 2: Web API Controller
builder.Services.AddControllers();

// Für Punkt 2: Entity Framework InMemory
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseInMemoryDatabase("InventoryDB"));

var app = builder.Build();

// Für Punkt 2: Controller-Routen aktivieren
app.MapControllers();

app.Run();


