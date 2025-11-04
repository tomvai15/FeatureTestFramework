using FeatureTestsFramework.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;

namespace FeatureTestsFramework
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario(Order = TestRunOrder.CreateServiceScope)]
        public static void BeforeScenario_CreateScopeForScenario(ScenarioContext scenarioContext)
        {
            var scenarioServiceScope = ServiceAccessor.ServiceProvider.CreateScope();
            scenarioContext.Set(scenarioServiceScope);
        }

        [AfterScenario(Order = TestRunOrder.DisposeServiceScope)]
        public static void AfterScenario_DisposeScope(ScenarioContext scenarioContext)
        {
            var scenarioServiceScope = scenarioContext.Get<IServiceScope>();
            scenarioServiceScope.Dispose();
        }
    }
}