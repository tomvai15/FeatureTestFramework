using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FeatureTestFramework.Tests.Bootstrapping;

public class ExampleApiWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // WebApplicationFactory can be configured here.
    }
}