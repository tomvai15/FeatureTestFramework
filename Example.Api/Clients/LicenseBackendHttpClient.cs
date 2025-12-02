using System.Text;
using System.Text.Json;
using Example.Api.Models;

namespace Example.Api.Clients;

public interface ILicenseBackendHttpClient
{
    Task PostNewLicense(CreateLicenseRequest request);
}

public class LicenseBackendHttpClient(HttpClient httpClient) : ILicenseBackendHttpClient
{
    public async Task PostNewLicense(CreateLicenseRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("PostNewLicense", content);
        //response.EnsureSuccessStatusCode();
        return;
    }
}