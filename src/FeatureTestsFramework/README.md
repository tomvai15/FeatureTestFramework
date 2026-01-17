FeatureTestsFramework
=====================

FeatureTestsFramework provides common HTTP feature-test helpers and wiring used with Reqnroll and xUnit-based feature tests.

This README explains the minimal setup for test projects so the NuGet package can register the test services and configuration the same way the project example does.

Why this package
----------------
- Registers test-friendly HTTP clients and supporting services (WireMock server, FeatureTestClient, etc.)
- Provides helpers for converting and formatting HTTP requests/responses in feature tests
- Keeps a single, simple bootstrap call that test projects can use to register services

Quick start
-----------
1. Add the NuGet package (or project reference) to your test project. The package includes required runtime dependencies such as Reqnroll and WireMock.

2. Add a small test startup class that will run before tests to register the package services. Example (copy to your test project):

```csharp
using FeatureTestsFramework;
using Reqnroll;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace YourTestsNamespace;

[Binding]
public class TestStartup
{
    [BeforeTestRun(Order = TestRunOrder.InjectServices)]
    public static void RegisterServices()
    {
        // TProgram should be the entry type of the API project you are testing, e.g. Program
        GlobalTestStartup.SetupHttpsSteps<Program>();
    }
}
```

Notes about the example above:
- The assembly-level xUnit attribute disables test parallelization to avoid port and WireMock conflicts in feature tests.
- The `[BeforeTestRun]` hook ensures the package wires up services once before any scenarios run.
- The generic `Program` argument refers to the entry type of the ASP.NET project under test. This matches how Microsoft.AspNetCore.Mvc.Testing and WebApplicationFactory typically identify the host.

Configuration
-------------
The library reads configuration via `FeatureTestsFramework.Bootstrapping.ConfigurationAccessor`, which by default loads configuration from:

- `appsettings.json`
- `appsettings.development.json` (optional)

If you need secrets during development you can call:

```csharp
FeatureTestsFramework.Bootstrapping.ConfigurationAccessor.AddUserSecrets<YourType>();
```

(for instance from a custom setup path before calling `SetupHttpsSteps<TProgram>()`).

Typical test configuration keys
-------------------------------
A test project can provide an `appsettings.Tests.json` (or copy `appsettings.json`) with settings the test clients will consume. Example values used in the sample tests:

```json
{
  "UseMockService": true,
  "FeatureTestClient": {
    "BaseUri": "https://localhost:7253"
  },
  "PostmanSettings": "http://localhost:5999/Postman/",
  "FeatureFlagSettings": "http://localhost:5999/FeatureFlagService/"
}
```

- `FeatureTestClient:BaseUri` controls the base address used by the FeatureTestClient when sending requests.
- Other settings in the package are read by typed clients (e.g. postman/feature flag/license backend client settings) and should be provided if your scenarios rely on them.

Test lifecycle and hooks
------------------------
The package registers hooks used by feature tests:
- A `BeforeScenario` hook creates a scoped service provider for each scenario and resets the WireMock server to ensure tests are isolated.
- An `AfterScenario` hook disposes the scope.
- An `AfterTestRun` hook can write captured WireMock logs to `wiremock-logs.json` for debugging test failures.

Best practices
--------------
- Keep `appsettings.Tests.json` in the test project and mark it to copy to output directory so tests can read configuration at runtime.
- Use the provided `FeatureTestClient` and typed HTTP clients rather than constructing raw HttpClient instances; they are wired to the test server and to the mock backend when configured.
- Disable test parallelization for the test assembly when running feature tests that use shared ports or shared mock servers.

Further notes
-------------
- The package aims to be light-weight and to integrate with Reqnroll/xUnit feature tests. If you need additional customization (for example, different registrations or startup ordering), you can call into the package bootstrap methods directly from your own test setup code.

License
-------
MIT


If you want, I can extend this README with more examples (common scenario patterns, sample `appsettings.Tests.json`, or a troubleshooting section).
