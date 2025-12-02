using System.Text;
using System.Text.Json;
using Example.Api.Models;

namespace Example.Api.Clients;

public interface IFeatureFlagHttpClient
{
    Task<GetFeatureFlagResponse> PostFeatureFlags(GetFeatureFlagRequest request);
}

public class FeatureFlagHttpClient(HttpClient httpClient) : IFeatureFlagHttpClient
{
    public async Task<GetFeatureFlagResponse> PostFeatureFlags(GetFeatureFlagRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("PostFeatureFlags", content);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<GetFeatureFlagResponse>())!;
    }
}