using FeatureTestsFramework.Extensions;
using Microsoft.Extensions.Configuration;

namespace FeatureTestsFramework.SOA.Tests
{
    public static class ConfigurationExtensions
    {
        public static bool IsUsingMockService(this IConfiguration configuration) 
            => bool.Parse(configuration.GetRequiredValue("UseMockService"));
    }
}
