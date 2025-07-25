using Microsoft.AspNetCore.Mvc;
using SensorAPI.BusinessLayer;
using SensorAPI.Models.Models;

namespace SensorAPI.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Gets a List of Sensors.
        /// </summary>
        /// <returns>A List of the sensors in the db</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/sensor
        ///     
        /// </remarks>
        /// /// <response code="200">Returns a list of sensors</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllSensors()
        {
           var sensors = await _sensorService.GetAllSensors();

           return Ok(sensors.ToList());
        }

        /// <summary>
        /// Gets a Sensor by id.
        /// </summary>
        /// <returns>A single Sensor with the supplied id or an error</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/sensor/1
        ///     
        /// </remarks>
        /// <response code="200">Returns the sensor</response>
        /// <response code="404">If the item is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorsById(int id)
        {
           var sensor = await _sensorService.GetSensorById(id);

           return Ok(sensor);
        }


        /// <summary>
        /// Where you can create a sensor.
        /// </summary>
        /// <returns>A created sensor or an error</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post api/sensor
        ///         {
        ///           "id": 1,
        ///           "name": "sensor 1",
        ///           "unit": "Celcius"
        ///         }
        ///     
        /// </remarks>
        /// <response code="200">Returns the saved sensor</response>
        /// <response code="400">If the items unit is not valid or if a sensor with the same name already exists</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Sensor>> CreateSensor(Sensor sensor)
        {
         
            var savedSensor = await _sensorService.AddSensor(sensor);
            // should probably return a 201
            //return CreatedAtAction(nameof(GetSensorsById), new { id = savedSensor.id }, savedSensor);
            return Ok(savedSensor);

        }
    }
}
