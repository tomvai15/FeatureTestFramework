using Example.Api.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestsFramework.SOA.Tests.Bootstrapping;

public class ExampleApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddHttpClient<IPostmanHttpClient, PostmanHttpClient>(options =>
                options.BaseAddress = new Uri("http://localhost:5999/postman")
            );
        });
    }
}