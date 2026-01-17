using FeatureTestFramework.Tests.Bootstrapping;
using FeatureTestsFramework;
using FeatureTestsFramework.Bootstrapping;
using Reqnroll;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace FeatureTestFramework.Tests;

[Binding]
public class TestStartup
{
    [BeforeTestRun(Order = TestRunOrder.InjectServices)]
    public static void RegisterServices()
    {
        var services = ServiceAccessor.ServiceCollection;
        services.AddCommonServices(ConfigurationAccessor.Configuration);
        services.AddWebApplicationFactory(ConfigurationAccessor.Configuration);
    }
}