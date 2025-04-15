namespace ComicBooks.SharedDtos;

public class SectionDto
{
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Genre { get; set; } = string.Empty;
    public Guid FloorPlanId { get; set; }
}