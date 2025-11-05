using FeatureTestsFramework.Exceptions;
using Microsoft.Extensions.Configuration;

namespace FeatureTestsFramework.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key)
    {
        var value = configuration[key];
        if (value == null)
        {
            throw new MissingConfigurationSectionException(key);
        }
        return value;
    }
}