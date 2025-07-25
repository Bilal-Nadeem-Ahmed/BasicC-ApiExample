using SensorAPI.Models.Models;

namespace SensorAPI.BusinessLayer
{
    public interface ISensorService
    {
        Task<IEnumerable<Sensor>> GetAllSensors();
        Task<Sensor?> GetSensorById(int id);
        Task<Sensor?> GetSensorByName(string name);
        Task<Sensor> AddSensor(Sensor sensor);
   
    }
}
