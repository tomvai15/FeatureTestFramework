namespace Example.Api.Models;

public class CreateLicenseRequest
{
    public required string ProductType { get; set; }
    public required string FeatureLevel { get; set; }
    public required string UserId { get; set; }
}