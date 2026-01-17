using FeatureTestsFramework.HttpRequest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestsFramework.Bootstrapping;

public static class WebApplicationFactoryRegistration
{
    public static IServiceCollection AddWebApplicationFactory<TEntryPoint>(this IServiceCollection services,
        IConfiguration configuration) where TEntryPoint : class
    {
        var factory = CreateFactory<TEntryPoint>();
        var client = factory.CreateClient();

        services.AddSingleton(factory);
        services.AddSingleton(client);

        services.AddWebApplicationFactoryHttpClient(configuration);

        return services;
    }

    private static WebApplicationFactory<TEntryPoint> CreateFactory<TEntryPoint>()
        where TEntryPoint : class
    {
        return new WebApplicationFactory<TEntryPoint>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureAppConfiguration((context, config) => config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Tests.json", optional: false, reloadOnChange: true));
            });
    }

    private static IServiceCollection AddWebApplicationFactoryHttpClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(FeatureTestClientConfiguration.SectionName);
        services.Configure<FeatureTestClientConfiguration>(configurationSection);
        var clientConfiguration = new FeatureTestClientConfiguration();
        configurationSection.Bind(clientConfiguration);

        services.AddSingleton<IFeatureTestClient, FeatureTestClient>();
        return services;
    }
}