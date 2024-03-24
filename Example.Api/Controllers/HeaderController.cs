using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("api/v1/headers")]
    public class HeaderController : ControllerBase
    {

        [HttpGet("needs-header/{header}")]
        public IActionResult NeedsHeader(string header)
        {
            if (Request.Headers.ContainsKey(header))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("returns-header/{header}/value/{value}")]
        public IActionResult ReturnsHeaderWithValue(string header, string value)
        {
            Response.Headers.Add(header, value);
            return Ok();
        }

        [HttpGet("returns-header/{header}")]
        public IActionResult ReturnsHeader(string header)
        {
            Response.Headers.Add(header, "any-value");
            return Ok();
        }


        [HttpGet("protected/{password}")]
        public IActionResult Protected(string password)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            if (Request.Headers.Authorization != password)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
