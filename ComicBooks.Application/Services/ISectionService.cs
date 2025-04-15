using ComicBooks.Domain.Entities;

namespace ComicBooks.Application.Services
{
    public interface ISectionService
    {
        Task<Section> CreateSectionAsync(string location, string genre, int capacity);
    }
}