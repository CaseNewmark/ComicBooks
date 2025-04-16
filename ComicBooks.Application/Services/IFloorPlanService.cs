using ComicBooks.Domain.Entities;

namespace ComicBooks.Application.Services
{
    public interface IFloorPlanService
    {
        Task<FloorPlan> CreateFloorPlanAsync(string name);
        Task AddSectionAsync(Guid floorPlanId, Section section);
        Task<FloorPlan> GetFloorPlanAsync(Guid floorPlanId);
        Task<List<FloorPlan>> GetAllFloorPlansAsync();
        Task DeleteFloorPlanAsync(Guid id);
        Task UpdateFloorPlanAsync(Guid id, string name, List<Section> sections);
    }
}