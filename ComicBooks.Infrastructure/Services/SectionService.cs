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

        public async Task<Section> CreateSectionAsync(string name, List<string> genres, int capacity)
        {
            var section = new Section
            {
                Name = name,
                Genres = genres,
                Capacity = capacity
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return section;
        }
    }
}