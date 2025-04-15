using ComicBooks.Domain.Entities;

namespace ComicBooks.Application.Services
{
    public interface ISectionService
    {
        Task<Section> CreateSectionAsync(string name, List<string> genres, int capacity);
    }
}