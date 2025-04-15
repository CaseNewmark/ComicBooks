using ComicBooks.Application.Services;
using ComicBooks.Infrastructure;
using ComicBooks.SharedDtos;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

// Register application services
builder.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// API endpoint to create a new floor plan
app.MapPost("/api/floorplans", async (IFloorPlanService floorPlanService, [FromBody]string name) =>
{
    var floorPlan = await floorPlanService.CreateFloorPlanAsync(name);
    return Results.Created($"/api/floorplans/{floorPlan.Id}", floorPlan);
});

app.MapGet("/api/floorplans", async (IFloorPlanService floorPlanService) =>
{
    var floorPlans = await floorPlanService.GetAllFloorPlansAsync();
    return Results.Ok(floorPlans);
});

app.MapGet("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id) =>
{
    var floorPlan = await floorPlanService.GetFloorPlanAsync(id);
    return floorPlan is not null ? Results.Ok(floorPlan) : Results.NotFound();
});

app.MapPut("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id, FloorPlanDto dto) =>
{
    await floorPlanService.UpdateFloorPlanAsync(id, dto.Name /*, ...sections... */);
    return Results.NoContent();
});

app.MapDelete("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id) =>
{
    await floorPlanService.DeleteFloorPlanAsync(id);
    return Results.NoContent();
});

app.MapFallback(() => {
    return Results.NotFound("Endpoint not found.");
});

app.MapDefaultEndpoints();

app.Run();
