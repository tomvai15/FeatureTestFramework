using System.Text.Json;
using FeatureTestsFramework.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using WireMock.Server;

namespace FeatureTestsFramework;

[Binding]
public class Hooks
{
    [BeforeScenario(Order = TestRunOrder.CreateServiceScope)]
    public static void BeforeScenario_CreateScopeForScenario(ScenarioContext scenarioContext)
    {
        var scenarioServiceScope = ServiceAccessor.ServiceProvider.CreateScope();
        scenarioContext.Set(scenarioServiceScope);
        var wireMockServer = scenarioServiceScope.ServiceProvider.GetRequiredService<WireMockServer>();
        wireMockServer.Reset();
    }

    [AfterScenario(Order = TestRunOrder.DisposeServiceScope)]
    public static void AfterScenario_DisposeScope(ScenarioContext scenarioContext)
    {
        var scenarioServiceScope = scenarioContext.Get<IServiceScope>();
        scenarioServiceScope.Dispose();
    }
    
    [AfterTestRun]
    public static void AfterTestRun()
    {
        var wireMockServer = ServiceAccessor.ServiceProvider.GetRequiredService<WireMockServer>();
        var mappedLogs = wireMockServer.LogEntries
            .Select(entry => new
            {
                // --- request ---
                Request = new
                {
                    Method = entry.RequestMessage.Method,
                    Url = entry.RequestMessage.Url,
                    Path = entry.RequestMessage.Path,
                    Body = entry.RequestMessage.Body
                },
                Response = new
                {
                    StatusCode = entry.ResponseMessage?.StatusCode,
                }
            })
            .ToList();
        
        var json = JsonSerializer.Serialize(
            mappedLogs,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText("wiremock-logs.json", json);
    }
}