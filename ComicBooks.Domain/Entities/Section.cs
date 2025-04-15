namespace ComicBooks.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public int Capacity { get; set; }
    }
}