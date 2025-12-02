using Example.Api.Clients;
using Example.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[ApiController]
[Route("")]
public class LicensesController(
    IFeatureFlagHttpClient featureFlagHttpClient,
    ILicenseBackendHttpClient licenseBackendHttpClient) : ControllerBase
{
    [HttpPost("PostLicense")]
    public async Task<IActionResult> PostLicense([FromBody] CreateLicenseRequest request)
    {
        var featureFlags = await featureFlagHttpClient.PostFeatureFlags(new GetFeatureFlagRequest
        {
            FeatureFlag = "create_licenses_enabled"
        });
        
        bool.TryParse(featureFlags.IsEnabled, out var enabled);

        if (!enabled)
        {
            return NotFound();
        }

        await licenseBackendHttpClient.PostNewLicense(request);

        return Ok();
    }
}