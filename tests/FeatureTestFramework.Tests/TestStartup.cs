using FeatureTestsFramework;
using Reqnroll;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace FeatureTestFramework.Tests;

[Binding]
public class TestStartup
{
    [BeforeTestRun(Order = TestRunOrder.InjectServices)]
    public static void RegisterServices()
    {
        GlobalTestStartup.SetupHttpsSteps<Program>();
    }
}