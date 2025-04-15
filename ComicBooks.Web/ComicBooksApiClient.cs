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
        var response = await _httpClient.PostAsJsonAsync("/api/floorplans", name);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FloorPlanDto>();
    }

    public async Task<List<FloorPlanDto>?> GetAllFloorPlansAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<FloorPlanDto>>("/api/floorplans");
    }

    public async Task<FloorPlanDto?> GetFloorPlanAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<FloorPlanDto>($"/api/floorplans/{id}");
    }

    public async Task UpdateFloorPlanAsync(FloorPlanDto floorPlan)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/floorplans/{floorPlan.Id}", floorPlan);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteFloorPlanAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/api/floorplans/{id}");
        response.EnsureSuccessStatusCode();
    }
}
