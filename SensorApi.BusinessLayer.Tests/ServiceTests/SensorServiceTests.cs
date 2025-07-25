
using SensorAPI.BusinessLayer.Exceptions;
using SensorAPI.BusinessLayer.Services;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorApi.BusinessLayer.Tests.ServiceTests
{
    public class SensorServiceTests
    {
        private Mock<ISensorRepository> _mockSensorRepository;
        private SensorService _sensorService;

    [SetUp]
        public void Setup()
        {
            _mockSensorRepository = new Mock<ISensorRepository>();
            _sensorService = new SensorService(_mockSensorRepository.Object);
        }

        [Test]
        public void AddSensor_WhithAnInvalidUnit_ShouldThrowBadRequest()
        {
            var sensorWithInvalidUnit = new Sensor
            {
                id = 1,
                name = "Test",
                unit = "Test"
            };

            Assert.ThrowsAsync<BadRequestException>(async () => await _sensorService.AddSensor(sensorWithInvalidUnit));
        }

        [Test]
        public void AddSensor_WhithAPreExistingName_ShouldThrowBadRequest()
        {
            var sensor1 = new Sensor
            {
                id = 1,
                name = "Test",
                unit = "Celcius"
            };

            var sensorWithDuplicateId = new Sensor
            {
                id = sensor1.id,
                name = "Test",
                unit = "Celcius"
            };
            
            _mockSensorRepository.Setup(repo => repo.GetSensorByName(sensor1.name)).ReturnsAsync(sensor1);
            Assert.ThrowsAsync<BadRequestException>(async () => await _sensorService.AddSensor(sensorWithDuplicateId));
        }

        [Test]
        public void GetSensorById_WhithAnInvalidId_ShouldThrowNotFound()
        {
            var id = 1;
            _mockSensorRepository.Setup(repo => repo.GetSensorById(id)).ReturnsAsync((Sensor)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _sensorService.GetSensorById(id));
        }

        [Test]
        public void GetSensorByName_WhithAnInvalidName_ShouldThrowNotFound()
        {
            var name = "Test";
            _mockSensorRepository.Setup(repo => repo.GetSensorByName(name)).ReturnsAsync((Sensor)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _sensorService.GetSensorByName(name));
        }

    }
}