using ComicBooks.Application.Services;
using ComicBooks.Domain.Entities;
using ComicBooks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicBooks.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly ComicBooksDbContext _context;

        public SectionService(ComicBooksDbContext context)
        {
            _context = context;
        }

        public async Task<Section> CreateSectionAsync(string location, string genre, int capacity)
        {
            var section = new Section
            {
                Location = location,
                Genre = genre,
                Capacity = capacity
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return section;
        }
    }
}