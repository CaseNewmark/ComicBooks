using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ComicBooks.SharedDtos;

public class ComicBooksApiClient
{
    private readonly HttpClient _httpClient;

    public ComicBooksApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FloorPlanDto?> CreateFloorPlanAsync(string name)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/floorplan", name);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FloorPlanDto>();
    }

    public async Task<List<FloorPlanDto>?> GetAllFloorPlansAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<FloorPlanDto>>("/api/floorplan");
    }

    public async Task<FloorPlanDto?> GetFloorPlanAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<FloorPlanDto>($"/api/floorplan/{id}");
    }

    public async Task UpdateFloorPlanAsync(FloorPlanDto floorPlan)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/floorplan/{floorPlan.Id}", floorPlan);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteFloorPlanAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/api/floorplan/{id}");
        response.EnsureSuccessStatusCode();
    }
}
