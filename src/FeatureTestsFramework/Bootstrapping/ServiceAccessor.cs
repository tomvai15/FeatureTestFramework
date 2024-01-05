using Microsoft.Extensions.DependencyInjection;

namespace FeatureTestsFramework.Bootstrapping
{
    public static class ServiceAccessor
    {
        public static readonly IServiceCollection ServiceCollection = new ServiceCollection();
        public static IServiceProvider ServiceProvider => lazyServiceProvider.Value;

        private static Lazy<IServiceProvider> lazyServiceProvider = new(ServiceCollection.BuildServiceProvider, LazyThreadSafetyMode.ExecutionAndPublication);
    }
}
