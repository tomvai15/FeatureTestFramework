using FeatureTestsFramework.Bootstrapping;
using FeatureTestsFramework.SOA.Tests.Bootstrapping;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.SOA.Tests
{
    [Binding]
    public class TestStartup
    {
        [BeforeTestRun(Order = TestRunOrder.InjectServices)]
        public static void RegisterServices()
        {
            ConfigurationAccessor.AddUserSecrets<TestStartup>();
            var services = ServiceAccessor.ServiceCollection;
            services.ConfigureServices(configuration: ConfigurationAccessor.Configuration);
        }
    }
}
