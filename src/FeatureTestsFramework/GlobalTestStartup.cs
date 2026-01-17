using FeatureTestsFramework.Bootstrapping;

namespace FeatureTestsFramework;

public static class GlobalTestStartup
{
    public static void SetupHttpsSteps<TProgram>() where TProgram : class
    {
        var services = ServiceAccessor.ServiceCollection;
        services.AddCommonServices<TProgram>(ConfigurationAccessor.Configuration);
    }
}