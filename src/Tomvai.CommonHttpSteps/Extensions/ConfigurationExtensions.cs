using Microsoft.Extensions.Configuration;
using Tomvai.CommonHttpSteps.Exceptions;

namespace Tomvai.CommonHttpSteps.Extensions;

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