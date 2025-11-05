using Example.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[ApiController]
[Route("api/v1/values")]
public class ValueController : ControllerBase
{
    [HttpPost("verify-dates")]
    public IActionResult VerifyDates([FromBody] ValidateDatesRequest dates)
    {
        const int error = 2;
        var now = DateTime.UtcNow;

        var isInvalid = dates.NoDate != default ||
                        dates.Future < now ||
                        dates.Past > now ||
                        !(now.AddSeconds(-error) < dates.Today && dates.Today < now.AddSeconds(error)) ||
                        (dates.Midnight - dates.Midnight.Date).Ticks != 0 ||
                        dates.JustAfterMidnight.Date < dates.JustAfterMidnight.Date && dates.JustAfterMidnight < dates.JustAfterMidnight.Date.AddSeconds(error) ||
                        dates.JustBeforeMidnight.Date.AddDays(1).AddSeconds(-error) < dates.JustBeforeMidnight.Date && dates.JustBeforeMidnight < dates.JustBeforeMidnight.Date.AddDays(1);

        if (isInvalid)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("verify-numbers")]
    public IActionResult VerifyNumbers([FromBody] ValidateNumbersRequest numbers)
    {
        var isInvalid = numbers.AnyId < 0 ||
                        numbers.NotExistingId != (long.MaxValue - 1) ||
                        numbers.AnyPositiveNumber <= 0 ||
                        numbers.AnyNegativeNumber >= 0 ||
                        numbers.AnyDecimal % 1 == 0;

        if (isInvalid)
        {
            return BadRequest();
        }

        return Ok();
    }
}