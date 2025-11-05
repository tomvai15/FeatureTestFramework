using Example.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[ApiController]
[Route("api/v1/protected-by-jwt")]
public class ProtectedController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetTruck()
    {
        return Ok();
    }
}