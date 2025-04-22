using ComicBooks.Application.Services;
using ComicBooks.Infrastructure;
using ComicBooks.SharedDtos;
using Microsoft.AspNetCore.Mvc;
using ComicBooks.Domain.Entities;
using ComicBooks.ApiService.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

// Automapper for the rescue of DTOs
builder.Services.AddAutoMapper(config => config.AddProfile<DtoMappingProfile>());

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
    return TypedResults.Created($"/api/floorplans/{floorPlan.Id}", floorPlan);
});

app.MapGet("/api/floorplans", async (IFloorPlanService floorPlanService, IMapper mapper) =>
{
    var floorPlans = await floorPlanService.GetAllFloorPlansAsync();
    return TypedResults.Ok(mapper.Map<List<FloorPlanDto>>(floorPlans));
});

app.MapGet("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id) =>
{
    var floorPlan = await floorPlanService.GetFloorPlanAsync(id);
    return floorPlan is not null ? TypedResults.Ok(floorPlan) : Results.NotFound();
});

app.MapPut("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id, FloorPlanDto dto) =>
{
    var sections = dto.Sections?.Select(s => new Section
    {
        Location = s.Location,
        Capacity = s.Capacity,
        Genre = s.Genre
    }).ToList() ?? new List<Section>();

    await floorPlanService.UpdateFloorPlanAsync(id, dto.Name, sections);
    return TypedResults.NoContent();
});

app.MapDelete("/api/floorplans/{id:guid}", async (IFloorPlanService floorPlanService, Guid id) =>
{
    await floorPlanService.DeleteFloorPlanAsync(id);
    return TypedResults.NoContent();
});

app.MapFallback(() => {
    return TypedResults.NotFound("Endpoint not found.");
});

app.MapDefaultEndpoints();

app.Run();
