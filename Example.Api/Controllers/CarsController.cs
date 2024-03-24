using Example.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cars")]
    public class CarsController : ControllerBase
    {
        private readonly List<Car> cars = new List<Car>()
        {
            new Car { Id = 1, Name="Car"}
        };

        [HttpGet("{carId}")]
        public IActionResult GetCar(int carId)
        {
            return Ok(cars.First());
        }

        [HttpDelete("{carId}")]
        public IActionResult DeleteCar(int carId)
        {
            return Ok();
        }

        [HttpPut("{carId}")]
        public IActionResult PutCar(int carId, [FromBody] Car car)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostCar([FromBody] CreateCarRequest car)
        {
            return Ok(car);
        }

        [HttpGet("{carId}/tires/{tireId}")]
        public IActionResult GetCarTire(int carId, int tireId)
        {
            return Ok();
        }

        [HttpPost("{carId}/tires")]
        public IActionResult PostTire(int carId, [FromBody] CreateTireRequest tire)
        {
            return Ok(tire);
        }

        [HttpGet("{carId}/tires/{tireId}/bolts/{boltId}")]
        public IActionResult GetCarTireBolt(int carId, int tireId, int boltId)
        {
            return Ok();
        }

        [HttpGet("/validate-query/{queryValue}")]
        public IActionResult GetQuery(string queryValue, [FromQuery] string include)
        {
            if (include != queryValue)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("/validate-query/{queryValue1}/{queryValue2}")]
        public IActionResult GetQuery(string queryValue1, string queryValue2, [FromQuery] string include1, [FromQuery] string include2)
        {
            if (include1 != queryValue1)
            {
                return BadRequest();
            }

            if (include2 != queryValue2)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
