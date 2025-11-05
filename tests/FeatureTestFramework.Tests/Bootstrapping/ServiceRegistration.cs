using FeatureTestsFramework.Bootstrapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestFramework.Tests.Bootstrapping;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddCommonServices(configuration);

        if (configuration.IsUsingMockService())
        {
            services.AddWebApplicationFactory(configuration);
        }

        return services;
    }
}