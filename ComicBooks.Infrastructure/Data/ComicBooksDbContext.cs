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
                entity.Property(s => s.Location).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Capacity).IsRequired();
                entity.Property(s => s.Genre).IsRequired().HasMaxLength(100);
                entity.HasOne(s => s.FloorPlan)
                      .WithMany(fp => fp.Sections)
                      .HasForeignKey(s => s.FloorPlanId);
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