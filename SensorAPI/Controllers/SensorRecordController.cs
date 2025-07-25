using Microsoft.AspNetCore.Mvc;
using SensorAPI.BusinessLayer;
using SensorAPI.Models.Models;

namespace SensorAPI.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class SensorRecordController : ControllerBase
    {
        private readonly ISensorRecordService _sensorRecordService;

        public SensorRecordController(ISensorRecordService sensorRecordService)
        {
            _sensorRecordService = sensorRecordService;
        }


        /// <summary>
        /// Gets all SensorRecords, has optional query params for filtering.
        /// </summary>
        /// <returns>A List of the SensorRecords in the db</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/data
        ///     
        /// </remarks>
        /// /// <response code="200">Returns a list of SensorRecords</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSensorRecordss(
            [FromQuery] DateTime? minDate,
            [FromQuery] DateTime? maxDate,
            [FromQuery] float? minValue,
            [FromQuery] float? maxValue)
        {
      
            var sensors = await _sensorRecordService.GetAllSensorRecords(minDate, maxDate, minValue, maxValue);
            // would probably add pagination in a larger project 

            return Ok(sensors.ToList());
        }


        /// <summary>
        /// Gets a SensorRecord by id.
        /// </summary>
        /// <returns>A single SensorRecord with the supplied id or an error</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/data/1
        ///     
        /// </remarks>
        /// <response code="200">Returns the SensorRecord</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorRecordById(int id)
        {
            var sensor = await _sensorRecordService.GetSensorRecordById(id);

            return Ok(sensor);
        }


        /// <summary>
        /// Where you can create a SensorRecord.
        /// </summary>
        /// <returns>A created sensorRecord or an error</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post api/data
        ///         {
        ///           "id": 1,
        ///           "sensor": "sensor 1",
        ///           "date": "2022-04-27 12:13"
        ///           "value: 12.0
        ///         }
        ///     
        /// </remarks>
        /// <response code="200">Returns the saved SensorRecord</response>
        /// <response code="400">If the items unit is not valid or if a sensor with the same name already exists</response>
        [HttpPost]
        public async Task<ActionResult<SensorRecord>> CreateSensorRecord(SensorRecord sensorRecord)
        {
            var savedSensorRecord = await _sensorRecordService.AddSensorRecord(sensorRecord);

            return Ok(savedSensorRecord);
        }
    }
}
