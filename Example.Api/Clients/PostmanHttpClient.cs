namespace Example.Api.Clients;

public interface IPostmanHttpClient
{
    Task<GetPostmanResponse> Get();
}

public class PostmanHttpClient(HttpClient _httpClient) : IPostmanHttpClient
{
    public async Task<GetPostmanResponse> Get()
    {
        var response = await _httpClient.GetAsync("get");
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<GetPostmanResponse>())!;
    }
}