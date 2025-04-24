using AutoMapper;
using ComicBooks.Application.Services;
using ComicBooks.Domain.Entities;
using ComicBooks.SharedDtos;
using Microsoft.AspNetCore.Mvc;

namespace ComicBooks.ApiService.Routes;

public static class FloorPlanRoutes
{
    public static void MapFloorPlanRoutes(this WebApplication app)
    {
        app.MapPost("/api/floorplan", async (IFloorPlanService floorPlanService, IMapper mapper, [FromBody] string name) =>
        {
            var floorPlan = await floorPlanService.CreateFloorPlanAsync(name);
            return TypedResults.Created($"/api/floorplan/{floorPlan.Id}", mapper.Map<FloorPlanDto>(floorPlan));
        })
        .WithName("CreateFloorPlan");

        app.MapGet("/api/floorplan", async (IFloorPlanService floorPlanService, IMapper mapper) =>
        {
            var floorPlans = await floorPlanService.GetAllFloorPlansAsync();
            return TypedResults.Ok(mapper.Map<List<FloorPlanDto>>(floorPlans));
        })
        .WithName("GetAllFloorPlans");

        app.MapGet("/api/floorplan/{id:guid}", async (IFloorPlanService floorPlanService, IMapper mapper, Guid id) =>
        {
            var floorPlan = await floorPlanService.GetFloorPlanAsync(id);
            return floorPlan is not null ? TypedResults.Ok(mapper.Map<FloorPlanDto>(floorPlan)) : Results.NotFound();
        })
        .WithName("GetFloorPlanById");

        app.MapPut("/api/floorplan/{id:guid}", async (IFloorPlanService floorPlanService, Guid id, FloorPlanDto dto) =>
        {
            var sections = dto.Sections?.Select(s => new Section
            {
                Location = s.Location,
                Capacity = s.Capacity,
                Genre = s.Genre
            }).ToList() ?? new List<Section>();

            await floorPlanService.UpdateFloorPlanAsync(id, dto.Name, sections);
            return TypedResults.NoContent();
        })
        .WithName("UpdateFloorPlan");

        app.MapDelete("/api/floorplan/{id:guid}", async (IFloorPlanService floorPlanService, Guid id) =>
        {
            await floorPlanService.DeleteFloorPlanAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteFloorPlan");
    }
}