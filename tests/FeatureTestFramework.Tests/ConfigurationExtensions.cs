using FeatureTestsFramework.Extensions;
using Microsoft.Extensions.Configuration;

namespace FeatureTestFramework.Tests;

public static class ConfigurationExtensions
{
    public static bool IsUsingMockService(this IConfiguration configuration) 
        => true;
}