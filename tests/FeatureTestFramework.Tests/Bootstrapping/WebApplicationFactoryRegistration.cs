using FeatureTestsFramework.HttpRequest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestFramework.Tests.Bootstrapping;

public static class WebApplicationFactoryRegistration
{
    public static IServiceCollection AddWebApplicationFactory(this IServiceCollection services, IConfiguration configuration)
    {
        var factory = new ExampleApiWebApplicationFactory();
        var client = factory.CreateClient();

        services.AddSingleton(factory);
        services.AddSingleton(client);

        services.AddWebApplicationFactoryHttpClient(configuration);

        return services;
    }

    public static IServiceCollection AddWebApplicationFactoryHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(FeatureTestClientConfiguration.SectionName);
        services.Configure<FeatureTestClientConfiguration>(configurationSection);
        var clientConfiguration = new FeatureTestClientConfiguration();
        configurationSection.Bind(clientConfiguration);

        services.AddSingleton<IFeatureTestClient, FeatureTestClient>();
        return services;
    }
}