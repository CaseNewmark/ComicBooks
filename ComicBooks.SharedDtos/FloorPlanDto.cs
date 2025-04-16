namespace ComicBooks.SharedDtos;

public class FloorPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
}