using Tomvai.CommonHttpSteps.Bootstrapping;

namespace Tomvai.CommonHttpSteps;

public static class GlobalTestStartup
{
    public static void SetupHttpsSteps<TProgram>() where TProgram : class
    {
        var services = ServiceAccessor.ServiceCollection;
        services.AddCommonServices<TProgram>(ConfigurationAccessor.Configuration);
    }
}