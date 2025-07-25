using SensorAPI.Models.Models;

namespace SensorAPI.DataLayer.Repositories.Interfaces
{
    public interface ISensorRepository
    {
        Task<IEnumerable<Sensor>> GetAllSensors();
        Task<Sensor?> GetSensorById(int id);
        Task<Sensor?> GetSensorByName(string name);
        Task<Sensor> AddSensor(Sensor sensor);
    }
}
