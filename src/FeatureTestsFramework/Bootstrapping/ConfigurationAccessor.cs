using Microsoft.Extensions.Configuration;

namespace FeatureTestsFramework.Bootstrapping
{
    public static class ConfigurationAccessor
    {
        private static IConfigurationBuilder configurationBuilder = CreateDefaultBuilder();
        private static Lazy<IConfigurationRoot> lazyConfiguration = new(configurationBuilder.Build, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IConfigurationRoot Configuration => lazyConfiguration.Value;
        
        public static void AddUserSecrets<T>() where T : class
        {
            configurationBuilder.AddUserSecrets<T>();
        }

        private static IConfigurationBuilder CreateDefaultBuilder()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.development.json", optional: true);
        }
    }
}
