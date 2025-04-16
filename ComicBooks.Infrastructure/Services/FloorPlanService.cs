using ComicBooks.Application.Services;
using ComicBooks.Domain.Entities;
using ComicBooks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicBooks.Infrastructure.Services
{
    public class FloorPlanService : IFloorPlanService
    {
        private readonly ComicBooksDbContext _context;

        public FloorPlanService(ComicBooksDbContext context)
        {
            _context = context;
        }

        public async Task<FloorPlan> CreateFloorPlanAsync(string name)
        {
            var floorPlan = new FloorPlan
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.FloorPlans.Add(floorPlan);
            await _context.SaveChangesAsync();

            return floorPlan;
        }

        public async Task AddSectionAsync(Guid floorPlanId, Section section)
        {
            var floorPlan = await _context.FloorPlans
                .Include(fp => fp.Sections)
                .FirstOrDefaultAsync(fp => fp.Id == floorPlanId);

            if (floorPlan == null)
            {
                throw new KeyNotFoundException($"FloorPlan with ID {floorPlanId} not found.");
            }

            floorPlan.Sections.Add(section);
            await _context.SaveChangesAsync();
        }

        public async Task<FloorPlan> GetFloorPlanAsync(Guid floorPlanId)
        {
            var floorPlan = await _context.FloorPlans
                .Include(fp => fp.Sections)
                .FirstOrDefaultAsync(fp => fp.Id == floorPlanId);

            if (floorPlan == null)
            {
                throw new KeyNotFoundException($"FloorPlan with ID {floorPlanId} not found.");
            }

            return floorPlan;
        }

        public async Task<List<FloorPlan>> GetAllFloorPlansAsync()
        {
            return await _context.FloorPlans
                .Include(fp => fp.Sections)
                .ToListAsync();
        }

        public async Task DeleteFloorPlanAsync(Guid id)
        {
            var floorPlan = await _context.FloorPlans.FindAsync(id);
            if (floorPlan != null)
            {
                _context.FloorPlans.Remove(floorPlan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateFloorPlanAsync(Guid id, string name, List<Section> sections)
        {
            var floorPlan = await _context.FloorPlans
                .Include(fp => fp.Sections)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (floorPlan == null)
                throw new KeyNotFoundException();

            floorPlan.Name = name;

            // Synchronize sections (simple approach: remove all and re-add)
            floorPlan.Sections.Clear();
            foreach (var sectionDto in sections)
            {
                floorPlan.Sections.Add(new Section
                {
                    Location = sectionDto.Location,
                    Capacity = sectionDto.Capacity,
                    Genre = sectionDto.Genre
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}