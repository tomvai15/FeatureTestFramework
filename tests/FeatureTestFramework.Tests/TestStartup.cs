using FeatureTestFramework.Tests.Bootstrapping;
using FeatureTestsFramework;
using FeatureTestsFramework.Bootstrapping;
using TechTalk.SpecFlow;

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
            services.ConfigureServices(ConfigurationAccessor.Configuration);
        }
    }
}
