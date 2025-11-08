using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace FeatureTestFramework.Tests.Bootstrapping;

public class ExampleApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Dictionary<string, string> configuration = new Dictionary<string, string>
    {
        { "PostmanSettings", "http://localhost:5999/Postman/" }
    };

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) => config.AddInMemoryCollection(configuration));
    }
}