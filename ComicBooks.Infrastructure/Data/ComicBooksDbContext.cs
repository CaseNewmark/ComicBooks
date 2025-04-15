using ComicBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicBooks.Infrastructure.Data
{
    public class ComicBooksDbContext : DbContext
    {
        public ComicBooksDbContext(DbContextOptions<ComicBooksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<FloorPlan> FloorPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Section entity
            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Capacity).IsRequired();
                entity.Property(s => s.Genres)
                      .HasConversion(
                          genres => string.Join(',', genres),
                          genres => genres.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                      );
            });

            // Configure FloorPlan entity
            modelBuilder.Entity<FloorPlan>(entity =>
            {
                entity.HasKey(fp => fp.Id);
                entity.Property(fp => fp.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(fp => fp.Sections)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}