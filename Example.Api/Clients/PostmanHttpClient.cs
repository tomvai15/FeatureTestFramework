namespace Example.Api.Clients;

public interface IPostmanHttpClient
{
    Task<GetPostmanResponse> Get();
}

public class PostmanHttpClient(HttpClient httpClient) : IPostmanHttpClient
{
    public async Task<GetPostmanResponse> Get()
    {
        var response = await httpClient.GetAsync("get");
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<GetPostmanResponse>())!;
    }
}