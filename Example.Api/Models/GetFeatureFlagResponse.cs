namespace Example.Api.Models;

public class GetFeatureFlagResponse
{
    public required string FeatureFlag { get; set; }
    public required bool IsEnabled { get; set; }
}