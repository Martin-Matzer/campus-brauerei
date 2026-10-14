using BrauereiApi.Services;
using BrauereiApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.Configure<InventoryBoundariesOptions>(
    builder.Configuration.GetSection(InventoryBoundariesOptions.SectionName));
builder.Services.AddScoped<CheckInventoryBoundariesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();