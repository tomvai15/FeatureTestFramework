using Example.Api.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("api/v1/external-api")]
    public class ExternalApiController(IPostmanHttpClient _postmanHttpClient) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _postmanHttpClient.Get();
            return Ok(response);
        }
    }
}
