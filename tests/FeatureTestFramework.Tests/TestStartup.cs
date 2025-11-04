using FeatureTestFramework.Tests.Bootstrapping;
using FeatureTestsFramework;
using FeatureTestsFramework.Bootstrapping;
using Reqnroll;

namespace FeatureTestFramework.Tests
{
    [Binding]
    public class TestStartup
    {
        [BeforeTestRun(Order = TestRunOrder.InjectServices)]
        public static void RegisterServices()
        {
            ConfigurationAccessor.AddUserSecrets<TestStartup>();
            var services = ServiceAccessor.ServiceCollection;
            services.AddCommonServices(ConfigurationAccessor.Configuration);
            services.ConfigureServices(ConfigurationAccessor.Configuration);
        }
    }
}
