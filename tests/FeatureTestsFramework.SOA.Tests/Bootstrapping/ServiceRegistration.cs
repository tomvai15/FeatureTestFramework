using FeatureTestsFramework.Bootstrapping;
using FeatureTestsFramework.SOA.Tests.Logic.Bootstrapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestsFramework.SOA.Tests.Bootstrapping
{
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

            services.AddWireMock();
            services.AddTokenGenerationServices();

            return services;
        }
    }
}
