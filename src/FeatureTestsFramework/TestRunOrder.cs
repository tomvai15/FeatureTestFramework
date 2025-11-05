namespace FeatureTestsFramework;

public class TestRunOrder
{
    public const int InjectServices = 0;
    public const int CreateServiceScope = 1;
    public const int ResolveServices = 2;
    public const int DisposeServiceScope = 99999;
}