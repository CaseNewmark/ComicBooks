using ComicBooks.Infrastructure;
using ComicBooks.ApiService.Services;
using ComicBooks.ApiService.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

// Automapper for the rescue of DTOs
builder.Services.AddAutoMapper(config => config.AddProfile<DtoMappingProfile>());

// Register application services
builder.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapFloorPlanRoutes();

app.MapFallback(() => {
    return TypedResults.NotFound("Endpoint not found.");
});

app.MapDefaultEndpoints();

app.Run();
