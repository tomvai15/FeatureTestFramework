using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T? GetService<T>(this ScenarioContext context)
            => context.Get<IServiceScope>().ServiceProvider.GetService<T>();

        public static T? GetRequiredService<T>(this ScenarioContext context) where T: notnull
            => context.Get<IServiceScope>().ServiceProvider.GetRequiredService<T>();
    }
}
