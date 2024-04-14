using FeatureTestsFramework.SOA.Tests.Logic.Token;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestsFramework.SOA.Tests.Logic.Bootstrapping
{
    public static class TokenGenerationRegistration
    {
        public static IServiceCollection AddTokenGenerationServices(this IServiceCollection services)
        {
            services.AddScoped<IFakeJwtTokenGenerator, FakeJwtTokenGenerator>();
            return services;
        }
    }
}
