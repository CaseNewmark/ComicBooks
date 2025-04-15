using ComicBooks.Application.Services;
using ComicBooks.Infrastructure.Data;
using ComicBooks.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ComicBooks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            // add database connection
            builder.AddNpgsqlDbContext<ComicBooksDbContext>(connectionName: "comicbooksdb");

            // Register infrastructure services here
            builder.Services.AddScoped<IFloorPlanService, FloorPlanService>();
            builder.Services.AddScoped<ISectionService, SectionService>();

            builder.Services.AddHostedService<DatabaseMigrationHostedService>();

            return builder;
        }
    }
}