using SensorAPI.BusinessLayer;
using SensorAPI.BusinessLayer.Exceptions;
using SensorAPI.BusinessLayer.Services;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorApi.BusinessLayer.Tests.ServiceTests
{
    public class SensorRecordServiceTests
    {
        private Mock<ISensorRecordRepository> _mockSensorRecordRepository;
        private Mock<ISensorService> _mockSensorService;
        private SensorRecordService _sensorRecordService;

        [SetUp]
        public void Setup()
        {
            _mockSensorRecordRepository = new Mock<ISensorRecordRepository>();
            _mockSensorService = new Mock<ISensorService>();
            _sensorRecordService = new SensorRecordService(
                _mockSensorRecordRepository.Object,
                _mockSensorService.Object
            );
        }

        [Test]
        public void AddSensorRecord_WhenSensorDoesNotExist_ShouldThrowNotFoundException()
        {
          
            var sensor = new SensorRecord
            {
                id = 1,
                sensor = "test",
                value = 10.5f,
                date = DateTime.UtcNow
            };

            _mockSensorService.Setup(s => s.GetSensorByName(sensor.sensor))
                .ThrowsAsync(new NotFoundException(""));

            Assert.ThrowsAsync<NotFoundException>(async () => await _sensorRecordService.AddSensorRecord(sensor));

        }
    }
}
