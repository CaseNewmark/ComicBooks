namespace ComicBooks.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Genre { get; set; } = string.Empty;
        public Guid FloorPlanId { get; set; }  // ?
        public FloorPlan? FloorPlan { get; set; } 
    }
}