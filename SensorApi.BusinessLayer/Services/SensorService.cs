using SensorAPI.BusinessLayer.Exceptions;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorAPI.BusinessLayer.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensors()
        {
            var sensors = await _sensorRepository.GetAllSensors();

            return sensors;
                
        }
        public async Task<Sensor?> GetSensorById(int id)
        {

            var sensor = await _sensorRepository.GetSensorById(id);
            if (sensor == null)
            {
                throw new NotFoundException($"Sensor with id {id} was not found.");
            }

            return sensor;
        }
        public async Task<Sensor?> GetSensorByName(string name)
        {
           
            var sensor = await _sensorRepository.GetSensorByName(name);
            if (sensor == null)
            {
                throw new NotFoundException($"Sensor with name {name} was not found.");
            }

            return sensor;
        }

        public async Task<Sensor> AddSensor(Sensor sensor)
        {
            List<string> allowedUnits = ["Celcius", "Fahrenheit"];

            if (!allowedUnits.Contains(sensor.unit))
            {
                throw new BadRequestException("Invalid Type For Unit, please return 'Celcius' or 'Fahrenheit'");
            }

            var existingSensorWithSameName = await _sensorRepository.GetSensorByName(sensor.name);
            if (existingSensorWithSameName != null)
            {
                throw new BadRequestException($"A sensor with the name '{sensor.name}' already exists");
            }

            return await _sensorRepository.AddSensor(sensor);
        }
      
    }
}
