using Microsoft.Extensions.DependencyInjection;
using Reqnroll;

namespace FeatureTestsFramework.Extensions;

public static class ServiceProviderExtensions
{
    public static T? GetService<T>(this IScenarioContext context)
        => (context as ScenarioContext)!.Get<IServiceScope>().ServiceProvider.GetService<T>();

    public static T? GetRequiredService<T>(this IScenarioContext context) where T: notnull
        => (context as ScenarioContext)!.Get<IServiceScope>().ServiceProvider.GetRequiredService<T>();
}