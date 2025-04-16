namespace ComicBooks.Domain.Entities
{
    public class FloorPlan
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}